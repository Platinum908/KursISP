using KursProjectISP31.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System;

namespace KursProjectISP31.Services
{
    public class RestaurantService
    {
        private readonly string _connectionString;

        public RestaurantService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public List<RestaurantTable> GetAvailableTables()
        {
            var tables = new List<RestaurantTable>();
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("GetAvailableTables", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(new RestaurantTable
                        {
                            Id = Convert.ToInt32(reader["id_столика"]),
                            HallId = Convert.ToInt32(reader["id_зала"]),
                            Status = Convert.ToInt32(reader["статус"]),
                            Seats = Convert.ToInt32(reader["колво_мест"])
                           
                        });
                    }
                }
            }
            return tables;
        }

        public List<MenuItem> GetMenu()
        {
            var menuItems = new List<MenuItem>();
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("GetMenu", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        menuItems.Add(new MenuItem
                        {
                            Id = reader.GetInt32("id_позиции"),
                            Name = reader.GetString("состав"),
                            Weight = Convert.ToInt32(reader["вес"]),
                            Price = reader.GetDecimal("стоимость"),
                            Category = reader.GetString("категория")
                        });
                    }
                }
            }
            return menuItems;
        }

        public bool BookTable(Booking newBooking)
        {
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("BookingTable", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@id_брони", newBooking.Id);
                command.Parameters.AddWithValue("@id_столика", newBooking.TableId);
                command.Parameters.AddWithValue("@имя", newBooking.CustomerName);
                command.Parameters.AddWithValue("@телефон", newBooking.PhoneNumber);
                command.Parameters.AddWithValue("@дата_время_брони", newBooking.BookingDateTime);
                command.Parameters.AddWithValue("@ограничение_времени", newBooking.TimeLimitHours);
                command.Parameters.AddWithValue("@комментарий", (object)newBooking.Comment ?? DBNull.Value);
                
                connection.Open();
                var result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        public int GetNextBookingId()
        {
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("SELECT ISNULL(MAX(id_брони), 0) + 1 FROM [dbo].[Booking]", connection);
                connection.Open();
                var result = command.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        public int GetNextOrderId()
        {
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("SELECT ISNULL(MAX(id_заказа), 0) + 1 FROM [dbo].[Orders]", connection);
                connection.Open();
                var result = command.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        public int GetNextOrderItemId()
        {
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("SELECT ISNULL(MAX(id_пробития), 0) + 1 FROM [dbo].[OrderItem]", connection);
                connection.Open();
                var result = command.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        public bool MakeOrder(Order newOrder)
        {
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("MakeOrder", connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddWithValue("@id_заказа", newOrder.Id);
                command.Parameters.AddWithValue("@id_столика", newOrder.TableId);
                command.Parameters.AddWithValue("@дата_время_заказа", newOrder.OrderDateTime);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool AddInOrder(OrderItem item)
        {
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("AddInOrder", connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddWithValue("@id_пробития", item.Id);
                command.Parameters.AddWithValue("@id_позиции", item.MenuItemId);
                command.Parameters.AddWithValue("@id_заказа", item.OrderId);
                command.Parameters.AddWithValue("@количество", item.Quantity);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public int GetNextBillId()
        {
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("SELECT ISNULL(MAX(id_счета), 0) + 1 FROM [dbo].[Bill]", connection);
                connection.Open();
                var result = command.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        public bool SaveBill(Bill bill)
        {
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("INSERT INTO [dbo].[Bill] (id_счета, id_заказа, id_столика, общая_сумма, дата_время_расчета) VALUES (@id, @orderId, @tableId, @total, @date)", connection);
                command.Parameters.AddWithValue("@id", bill.Id);
                command.Parameters.AddWithValue("@orderId", bill.OrderId);
                command.Parameters.AddWithValue("@tableId", bill.TableId);
                command.Parameters.AddWithValue("@total", bill.TotalAmount);
                command.Parameters.AddWithValue("@date", bill.PaymentDateTime);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public DailyReport GetDailyReport(DateTime date)
        {
            DailyReport report = null;
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("GetloadingRestaurant", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@дата_выручки", date.Date);
                
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        report = new DailyReport
                        {
                            LoadPercentage = reader["загрузка"] != DBNull.Value ? Convert.ToDecimal(reader["загрузка"]) : 0,
                            TotalRevenue = reader["общая_выручка"] != DBNull.Value ? Convert.ToDecimal(reader["общая_выручка"]) : 0
                        };
                    }
                }
            }
            return report;
        }

        public List<RestaurantTable> GetAvailableTablesOnDate(DateTime date)
        {
            var tables = new List<RestaurantTable>();
            var query = @"
                SELECT * FROM [dbo].[Tables]
                WHERE id_столика NOT IN (
                    SELECT id_столика FROM [dbo].[Booking]
                    WHERE CAST(дата_время_брони AS DATE) = @date
                )";

            using (var connection = CreateConnection())
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", date.Date);
                
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(new RestaurantTable
                        {
                            Id = Convert.ToInt32(reader["id_столика"]),
                            HallId = Convert.ToInt32(reader["id_зала"]),
                            Status = Convert.ToInt32(reader["статус"]),
                            Seats = Convert.ToInt32(reader["колво_мест"])
                        });
                    }
                }
            }
            return tables;
        }

        public List<OrderItem> GetAllOrders()
        {
            var items = new List<OrderItem>();
            var query = @"
                SELECT oi.id_пробития, oi.id_заказа, m.состав, oi.количество, oi.статус 
                FROM [dbo].[OrderItem] oi
                JOIN [dbo].[Menu] m ON oi.id_позиции = m.id_позиции
                ORDER BY oi.id_заказа";

            using (var connection = CreateConnection())
            {
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new OrderItem
                        {
                            Id = Convert.ToInt32(reader["id_пробития"]),
                            OrderId = Convert.ToInt32(reader["id_заказа"]),
                            Name = reader["состав"].ToString(),
                            Quantity = Convert.ToInt32(reader["количество"]),
                            Status = Convert.ToInt32(reader["статус"])
                        });
                    }
                }
            }
            return items;
        }

        public bool UpdateOrderItemStatus(int orderItemId, int newStatus)
        {
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("UPDATE [dbo].[OrderItem] SET статус = @status WHERE id_пробития = @id", connection);
                command.Parameters.AddWithValue("@status", newStatus);
                command.Parameters.AddWithValue("@id", orderItemId);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
} 
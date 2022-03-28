using System;
using HotelTask.Models;

namespace HotelTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {
            Console.WriteLine("-------------------------------------------\n" +
                             $"----------- Hotel management app ----------\n" +
                              "-------------------------------------------");
            Console.WriteLine("-------------------------------------------\n" +
                             $"------------- Create new hotel ------------\n" +
                              "-------------------------------------------");
            string hotelName = GetStringInputByConsole("hotel name");
            Hotel hotel = new Hotel(hotelName);
            Console.WriteLine("-------------------------------------------\n" +
                             $"-------- Hotel successfully created -------\n" +
                              "-------------------------------------------");

        Menu:
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("1 - Create new room\n" +
                              "2 - Pring all rooms\n" +
                              "3 - Add room to hotel by id\n" +
                              "4 - Remove room from hotel by id\n" +
                              "5 - Print rooms in hotel\n" +
                              "6 - Make reservation\n" +
                              "7 - Cancel reservation\n" +
                              "0 - Quit and close");
            Console.WriteLine("");
            Console.WriteLine("-------------------------------------------");

            int input = GetIntInputByConsole("input");

            switch (input)
            {
                case 1:
                    Console.Clear();
                    Room.CreateRoomByConsole();
                    goto Menu;
                case 2:
                    Console.Clear();
                    Room.PrintAllRooms();
                    goto Menu;
                case 3:
                    Console.Clear();
                    Hotel.AddRoomById();
                    goto Menu;
                case 4:
                    Console.Clear();
                    Hotel.RemoveRoomById();
                    goto Menu;
                case 5:
                    Console.Clear();
                    Hotel.PrintAllRoomsInHotel();
                    goto Menu;
                case 6:
                    Console.Clear();
                    Hotel.MakeReservationById();
                    goto Menu;
                case 7:
                    Console.Clear();
                    Hotel.CancelReservationById();
                    goto Menu;
                case 0:
                    return;
                default:
                    Console.Clear();
                    goto Menu;
            }
                
        }

        public static string GetStringInputByConsole(string content)
        {
        ReEnterStringInput:
            Console.Write($"Please enter {content} : ");
            string input = Console.ReadLine().Trim().ToLower();
            if (string.IsNullOrEmpty(input))
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------------\n" +
                                 $"--------- {content} can't be empty! --------\n" +
                                  "-------------------------------------------");
                goto ReEnterStringInput;
            }

            return input;
        }

        public static int GetIntInputByConsole(string content)
        {
        ReEnterIntInput:

            Console.Write($"Please enter {content} : ");
            string inputString = Console.ReadLine().Trim().ToLower();
            int? input;

            try
            {
                input = Convert.ToInt32(inputString);
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------------\n" +
                                  $"------- {content} must be digit! ------\n" +
                                  "-------------------------------------------");
                goto ReEnterIntInput;
            }

            return (int)input;
        }
    }
}

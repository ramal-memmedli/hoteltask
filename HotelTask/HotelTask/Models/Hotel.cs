using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelTask.Exceptions;

namespace HotelTask.Models
{
    public class Hotel
    {
        public string Name { get; set; }

        private static Room[] _rooms;

        private Hotel()
        {
            _rooms = new Room[0];
        }

        public Hotel(string name) : this()
        {
            Name = name;
        }

        public static void AddRoom(Room room)
        {
            Array.Resize(ref _rooms, _rooms.Length + 1);
            _rooms[^1] = room;
        }

        public static void RemoveRoom(Room room, int id)
        {
            if (_rooms.Contains(room))
            {
                Room roomForRemove = Array.Find(_rooms, room => room.RoomId == id);
                int indexOfRoomForRemove = Array.IndexOf(_rooms, roomForRemove);
                _rooms[indexOfRoomForRemove] = _rooms[^1];
                Array.Resize(ref _rooms, _rooms.Length - 1);
                Console.Clear();
                Console.WriteLine("-------------------------------------------\n" +
                                 $"-------- Room removed successfully --------\n" +
                                  "-------------------------------------------");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------------\n" +
                                 $"------ Selected room is not in hotel ------\n" +
                                  "-------------------------------------------");
            }

            
        }

        public static void AddRoomById()
        {
        TryAgain:

            Room.PrintAllRooms();
            int id = Program.GetIntInputByConsole("id of room");

            if(id == 0)
            {
                Console.Clear();
                return;
            }

            if (!Room.CheckId(id))
            {
                Console.WriteLine("-------------------------------------------\n" +
                                 $"----------- Room doesn't exist! -----------\n" +
                                  "----- Re-enter id or press 0 for exit -----\n" +
                                  "-------------------------------------------");
                goto TryAgain;
            }

            AddRoom(Room.GetRoom(id));

            Console.WriteLine("-------------------------------------------\n" +
                             $"--------- Room successfully added ---------\n" +
                              "-------------------------------------------");
        }

        public static void RemoveRoomById()
        {
        TryAgain:
            Console.Clear();
            PrintAllRoomsInHotel();
            Console.WriteLine("-------------------------------------------\n" +
                             $"--------------- Select room ---------------\n" +
                              "------------- Press 0 for exit ------------\n" +
                              "-------------------------------------------");

            int id = Program.GetIntInputByConsole("id of room");

            if(id == 0)
            {
                Console.Clear();
                return;
            }

            if (!Room.CheckId(id))
            {
                Console.WriteLine("-------------------------------------------\n" +
                                 $"----------- Room doesn't exist! -----------\n" +
                                  "----- Re-enter id or press 0 for exit -----\n" +
                                  "-------------------------------------------");
                goto TryAgain;
            }

            RemoveRoom(Room.GetRoom(id), id);

            Console.WriteLine("-------------------------------------------\n" +
                             $"-------- Room successfully removed --------\n" +
                              "-------------------------------------------");
        }

        public static void PrintAllRoomsInHotel()
        {
            if(_rooms.Length == 0)
            {
                Console.WriteLine("-------------------------------------------\n" +
                                 $"-------- There is no room in hotel --------\n" +
                                  "-------------------------------------------");
            }
            else
            {
                Console.WriteLine("-------------------------------------------\n" +
                             $"----------- All rooms in hotel -----------\n" +
                              "-------------------------------------------");
                foreach (Room room in _rooms)
                {
                    Console.WriteLine($"ID - {room.RoomId} | Room name - {room.RoomName} | Room price - {room.Price} | Person capacity of room - {room.PersonCapacity} | Is room available for reservation? - {room.IsAvailable}");
                }
            }
            
        }

        public static void MakeReservation(int? roomId)
        {
            if(roomId == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                if (Array.Exists(_rooms, room => room.RoomId == roomId))
                {
                    Room room = Array.Find(_rooms, room => room.RoomId == roomId);
                    int index = Array.IndexOf(_rooms, room);
                    if (_rooms[index].IsAvailable == false)
                    {
                        throw new NotAvailableException("Selected room already reserved.");
                    }
                    else
                    {
                        _rooms[index].IsAvailable = false;
                    }
                }
                else
                {
                    Console.WriteLine("-------------------------------------------\n" +
                                     $"------ There is no room with this id! -----\n" +
                                      "-------------------------------------------");
                }
            }
        }

        public static void CancelReservation(int? roomId)
        {
            if(roomId == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                if (Array.Exists(_rooms, room => room.RoomId == roomId))
                {
                    Room room = Array.Find(_rooms, room => room.RoomId == roomId);
                    int index = Array.IndexOf(_rooms, room);
                    if (_rooms[index].IsAvailable == true)
                    {
                        throw new NotAvailableException("Selected room never reserved.");
                    }
                    else
                    {
                        _rooms[index].IsAvailable = true;
                    }
                }
                else
                {
                    Console.WriteLine("-------------------------------------------\n" +
                                     $"------ There is no room with this id! -----\n" +
                                      "-------------------------------------------");
                }
            }
        }

        public static void MakeReservationById()
        {
            TryAgain:
            PrintAllRoomsInHotel();

            int id = Program.GetIntInputByConsole("id of room");

            if(id == 0)
            {
                Console.Clear();
                return;
            }

            if(!Array.Exists(_rooms, room => room.RoomId == id))
            {
                Console.WriteLine("-------------------------------------------\n" +
                                 $"----------- Room doesn't exist! -----------\n" +
                                  "----- Re-enter id or press 0 for exit -----\n" +
                                  "-------------------------------------------");
                goto TryAgain;
            }

            MakeReservation(id);

            Console.WriteLine("-------------------------------------------\n" +
                             $"----- Room with id{id} reserved for you -----\n" +
                              "-------------------------------------------");
        }

        public static void CancelReservationById()
        {
        TryAgain:
            PrintAllRoomsInHotel();

            int id = Program.GetIntInputByConsole("id of room");

            if (id == 0)
            {
                Console.Clear();
                return;
            }

            if (!Array.Exists(_rooms, room => room.RoomId == id))
            {
                Console.WriteLine("-------------------------------------------\n" +
                                 $"----------- Room doesn't exist! -----------\n" +
                                  "----- Re-enter id or press 0 for exit -----\n" +
                                  "-------------------------------------------");
                goto TryAgain;
            }

            CancelReservation(id);

            Console.WriteLine("-------------------------------------------\n" +
                             $"Reservation for room with id{id} cancelled\n" +
                              "-------------------------------------------");

        }
    }
}

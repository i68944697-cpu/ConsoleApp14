using System;
using System.Collections.Generic;

namespace CinemaBookingSystem
{
   
    public class Movie
    {
        public string Title { get; }
        public int DurationMinutes { get; }

        public Movie(string title, int durationMinutes)
        {
            Title = title;
            DurationMinutes = durationMinutes;
        }
    }

    
    public class Seat
    {
        public int Number { get; }
        public bool IsReserved { get; private set; }

        public Seat(int number)
        {
            Number = number;
            IsReserved = false;
        }

        
        public bool Reserve()
        {
            if (IsReserved)
            {
                return false; 
            }

            IsReserved = true;
            return true; 
        }
    }

    

    
    public class CinemaHall
    {
        public string Name { get; }
        public List<Seat> Seats { get; }

        public CinemaHall(string name, int totalSeats)
        {
            Name = name;
            Seats = new List<Seat>();

            
            for (int i = 1; i <= totalSeats; i++)
            {
                Seats.Add(new Seat(i)); 
            }
        }

       
        public void DisplaySeatMap()
        {
            Console.WriteLine($"\n--- Схема зала ({Name}) ---");
            Console.WriteLine("[ СЦЕНА / ЭКРАН ]");

            foreach (var seat in Seats)
            {
               
                string seatView = seat.IsReserved ? "[X]" : $"[{seat.Number}]";
                Console.Write($"{seatView} ");
            }
            Console.WriteLine("\n---------------------------");
        }
    }

   

    public class BookingService
    {
        private List<Movie> _movies;
        private CinemaHall _hall;

        public BookingService(List<Movie> movies, CinemaHall hall)
        {
            _movies = movies;
            _hall = hall;
        }

        public void Start()
        {
            Console.WriteLine("Добро пожаловать в систему бронирования кинотеатра!");

            
            Movie selectedMovie = ChooseMovie();
            if (selectedMovie == null) return;

            Console.Clear();
            Console.WriteLine($"Вы выбрали фильм: {selectedMovie.Title} ({selectedMovie.DurationMinutes} мин.)");

            
            _hall.DisplaySeatMap();

           
            ProcessBooking();

           
            Console.WriteLine("\nОбновленная схема зала после вашей операции:");
            _hall.DisplaySeatMap();

            Console.WriteLine("\nСпасибо за использование нашей системы! Нажмите любую клавишу для выхода.");
            Console.ReadKey();
        }

        private Movie ChooseMovie()
        {
            Console.WriteLine("\nДоступные фильмы:");
            for (int i = 0; i < _movies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_movies[i].Title} ({_movies[i].DurationMinutes} мин.)");
            }

            while (true)
            {
                Console.Write("\nВыберите номер фильма: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= _movies.Count)
                {
                    return _movies[choice - 1];
                }
                Console.WriteLine("Некорректный ввод. Пожалуйста, выберите номер из списка.");
            }
        }

        private void ProcessBooking()
        {
            while (true)
            {
                Console.Write("\nВведите номер места для бронирования: ");
                if (int.TryParse(Console.ReadLine(), out int seatNumber) && seatNumber >= 1 && seatNumber <= _hall.Seats.Count)
                {
                   
                    Seat seatToReserve = _hall.Seats[seatNumber - 1];

                   
                    if (seatToReserve.Reserve())
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Успешно! Место №{seatNumber} забронировано за вами.");
                        Console.ResetColor();
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Ошибка! Место №{seatNumber} уже занято. Выберите другое.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.WriteLine($"Некорректный номер места. В зале всего {_hall.Seats.Count} мест.");
                }
            }
        }
    }

   

    class Program
    {
        static void Main(string[] args)
        {
            
            List<Movie> movies = new List<Movie>
            {
                new Movie("Интерстеллар", 169),
                new Movie("Начало", 148),
                new Movie("Матрица", 136),
                new Movie("Во все тяжние", 139),
                 new Movie("Cосед", 135)
            };

            
            CinemaHall mainHall = new CinemaHall("Главный зал", 10);

           
            
            mainHall.Seats[2].Reserve(); 
            mainHall.Seats[5].Reserve(); 

            
            BookingService bookingService = new BookingService(movies, mainHall);
            bookingService.Start();
        }
    }
}

using System;
using System.Collections.Generic;
using ConferenceTrackManagement.Controller;
using ConferenceTrackManagement.Model;

namespace ConferenceTrackManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            TalkController talkController = new TalkController();
            SchedulingController schedulingController = new SchedulingController();
            int numberSelected = 0;
            Console.WriteLine("Welcome to CONFERENCE TRACK MANAGEMENT");
            Console.WriteLine("");
            while (numberSelected != 9)
            {
                Console.WriteLine("");
                Console.WriteLine("------------------------------------------------------------------------------");
                Console.WriteLine("Select an option:");
                Console.WriteLine("1 - Create default talks");
                Console.WriteLine("2 - Scheduling talks");
                Console.WriteLine("3 - Add talk");
                Console.WriteLine("4 - List all Talks");
                Console.WriteLine("9 - Exit");
                Console.WriteLine("------------------------------------------------------------------------------");
                string optionSelected = Console.ReadLine();
                if (int.TryParse(optionSelected, out numberSelected))
                {
                    switch (numberSelected)
                    {
                        case 1:
                            Console.Clear();
                            talkController.CreateDefaultTalks();
                            Console.WriteLine("Default talk were created successfully.");
                            break;
                        case 2:
                            try
                            {
                                Console.Clear();
                                Console.WriteLine("Which date start the conference? (YYYY/MM/DD)");
                                string firstDayConferenceString = Console.ReadLine();
                                DateTime firstDayConference = new DateTime();
                                //validate date format
                                if (DateTime.TryParse(firstDayConferenceString, out firstDayConference))
                                {
                                    Console.WriteLine("Building Scheduling...");
                                    //get the best scheduling 
                                    List<Track> bestTrackList = schedulingController.ScheduleTalks(firstDayConference);
                                    Console.Clear();
                                    Console.WriteLine("This is the best scheduling of talks.");
                                    Console.WriteLine("");
                                    foreach (Track track in bestTrackList)
                                    {
                                        Console.WriteLine("--------------" + track.Title + "--------------");
                                        Console.WriteLine("");
                                        foreach (Session session in track.SessionList)
                                        {
                                            Console.WriteLine("--------------" + session.Title + "--------------");
                                            Console.WriteLine("");
                                            foreach (Scheduling scheduling in session.SchedulingList)
                                            {
                                                Console.WriteLine(scheduling.StartHour.ToString("HH:mm") + "-" + scheduling.EndHour.ToString("HH:mm") + " > " + scheduling.Talk.Title + ", " + scheduling.Talk.Duration + " MIN");
                                            }
                                            Console.WriteLine("");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid date! Please write a date in format YYYY/MM/DD");
                                }
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception.Message);
                                Console.WriteLine("");
                                Console.WriteLine("Please try again.");
                            }
                            break;
                        case 3:
                            Talk talk = new Talk();
                            Console.Clear();
                            Console.WriteLine("Add a talk");
                            Console.WriteLine("Add a title to your talk: ");
                            talk.Title = Console.ReadLine();

                            Console.WriteLine("Is lightining talk? (1-Yes, 2-No) ");
                            string isLightiningString = Console.ReadLine();
                            while (!(isLightiningString == "1" || isLightiningString == "2"))
                            {
                                Console.WriteLine("Invalid value! Please write 1 to YES or 2 to NO.");
                                Console.WriteLine("Is lightining talk? (1-Yes, 2-No) ");
                                isLightiningString = Console.ReadLine();
                            }

                            if (isLightiningString == "1" ? true : false)
                            {
                                talk.IsLightning = true;
                                talk.Duration = 5;
                            }
                            else
                            {
                                Console.WriteLine("Add a time, in minutes, to your talk: ");
                                string time = Console.ReadLine();
                                int timeInt;
                                while (!int.TryParse(time, out timeInt) || (timeInt > 180))
                                {
                                    Console.WriteLine("Invalid time! Please write a time only with numbers and lenght max 180 minutes.");
                                    Console.WriteLine("Add in minutes a time to your talk: ");
                                    time = Console.ReadLine();
                                }
                                talk.IsLightning = false;
                                talk.Duration = timeInt;
                            }
                            try
                            {
                                if (talkController.AddTalk(talk))
                                {
                                    Console.Clear();
                                    Console.WriteLine("Talk add successfuly.");
                                }
                            }
                            catch (Exception talkException)
                            {
                                Console.Clear();
                                Console.WriteLine(talkException.Message);
                            }
                            break;
                        case 4:
                            List<Talk> talkList = talkController.ListAllTalks();
                            Console.Clear();
                            Console.WriteLine("List of talks");
                            Console.WriteLine("");
                            foreach (Talk talkObject in talkList)
                            {
                                Console.WriteLine(talkObject.Title+" - "+talkObject.Duration);
                            }
                            Console.WriteLine("----------------------------------------------------------------------");
                            Console.WriteLine(talkList.Count+" talks listed.");
                            Console.WriteLine("");
                            break;
                        default:
                            Console.WriteLine("Command not found.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("You need write a valid number.");
                }
            }
        }
    }
}

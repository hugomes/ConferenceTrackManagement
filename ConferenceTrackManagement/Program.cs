using System;
using System.Collections.Generic;
using System.Linq;
using ConferenceTrackManagement.Controller;
using ConferenceTrackManagement.Library;
using ConferenceTrackManagement.Model;

namespace ConferenceTrackManagement
{
    class Program
    {
        private static TalkController _talkController = new TalkController();
        private static SchedulingController _schedulingController = new SchedulingController();
        static void Main(string[] args)
        {
            int numberSelected = 0;
            Console.WriteLine("Welcome to CONFERENCE TRACK MANAGEMENT");
            Console.WriteLine("");
            while (numberSelected != 9)
            {
                string optionSelected = WriteOptions();
                if (int.TryParse(optionSelected, out numberSelected))
                {
                    switch (numberSelected)
                    {
                        case 1:
                            CreateDefaultTalk();
                            break;
                        case 2:
                            ScheduleConferenceTalks();
                            break;
                        case 3:
                            AddTalk();
                            break;
                        case 4:
                            ListAllTalks();
                            break;
                        default:
                            Console.WriteLine(ExceptionsMessages.Program_Main_Command_not_found_);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(ExceptionsMessages.Program_Main_You_need_write_a_valid_number_);
                }
            }
        }

        private static void ListAllTalks()
        {
            List<Talk> talkList = _talkController.ListAllTalks();
            Console.Clear();
            Console.WriteLine("List of talks");
            Console.WriteLine("");
            foreach (Talk talkObject in talkList)
            {
                Console.WriteLine(talkObject.Title + " - " + talkObject.Duration);
            }
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine(talkList.Count + " talks listed.");
            Console.WriteLine("");
        }

        private static string WriteOptions()
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
            return optionSelected;
        }

        private static void AddTalk()
        {
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
                if (_talkController.AddTalk(talk))
                {
                    Console.Clear();
                    Console.WriteLine(ExceptionsMessages.Program_AddTalk_Talk_add_successfuly_);
                }
            }
            catch (Exception talkException)
            {
                Console.Clear();
                Console.WriteLine(talkException.Message);
            }
        }

        private static void CreateDefaultTalk()
        {
            Console.Clear();
            _talkController.CreateDefaultTalks();
            Console.WriteLine("Default talk were created successfully.");
        }

        private static void ScheduleConferenceTalks()
        {
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
                    List<Track> bestTrackList = _schedulingController.ScheduleTalks(firstDayConference);
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
                                Console.WriteLine(scheduling.StartHour.ToString("HH:mm") + "-" +
                                                  scheduling.EndHour.ToString("HH:mm") + " > " +
                                                  scheduling.Talk.Title + ", " + scheduling.Talk.Duration + " MIN");
                            }

                            Console.WriteLine("");
                        }
                    }
                }
                else
                {
                    Console.WriteLine(ExceptionsMessages.Program_ScheduleConferenceTalks_Invalid_date__Please_write_a_date_in_format_YYYY_MM_DD);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("");
                Console.WriteLine(ExceptionsMessages.Program_ScheduleConferenceTalks_Please_try_again_);
            }
        }
    }
}

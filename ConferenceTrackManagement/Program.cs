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
                        case 5:
                            AddPersonAtTalk();
                            break;
                        default:
                            Console.WriteLine(ExceptionsMessages.EXCEPTION_COMMAND_NOT_FOUND);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(ExceptionsMessages.MESSAGE_INVALID_NUMBER);
                }
            }
        }

        private static int ListAllTalks()
        {
            IList<Talk> talkList = _talkController.ListAllTalks();
            Console.Clear();
            Console.WriteLine("List of talks");
            Console.WriteLine("");
            for (int i = 0; i < talkList.Count; i++)
            {
                Console.WriteLine($"{i+1} - {talkList[i].Title} - {talkList[i].Duration}");
            }

            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine(talkList.Count + " talks listed.");
            Console.WriteLine("");
            return talkList.Count;
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
            Console.WriteLine("5 - Add a Person at Talks");
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
                    Console.WriteLine(ExceptionsMessages.MESSAGE_TALK_ADD_SUCCESSFULY);
                }
            }
            catch (Exception talkException)
            {
                Console.Clear();
                Console.WriteLine(talkException.Message);
            }
        }

        private static void AddPersonAtTalk()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Add a the Person at talk");

                Console.WriteLine("Write the name of the Person: ");
                string Name = Console.ReadLine();

                int qtdTalks = ListAllTalks();

                Console.WriteLine("Write the number of the Talk above to add the Person");
                string numberString = Console.ReadLine();
                int numberInt;
                while (!int.TryParse(numberString, out numberInt) && (numberInt >= 1 && numberInt <= qtdTalks))
                {
                    Console.WriteLine(ExceptionsMessages.MESSAGE_INVALID_TALK_INDEX, qtdTalks);
                    numberString = Console.ReadLine();
                }

                PersonController personController = new PersonController();
                Person person = personController.AddPerson(Name);

                Talk talk = _talkController.GetTalkByIndex(numberInt-1);
                _talkController.AddAudience(person, talk);
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Person {person.Name} add successfully at {talk.Title}.");
                Console.ForegroundColor = ConsoleColor.White;
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
                    Console.WriteLine(ExceptionsMessages.MESSAGE_INVALID_DATE);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("");
                Console.WriteLine(ExceptionsMessages.MESSAGE_TRY_AGAIN);
            }
        }
    }
}

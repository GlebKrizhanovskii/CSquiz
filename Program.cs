﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace ConsoleApp1
{
	public partial class Program
	{
		static void Main(string[] args)
		{


			UsersCollection users = new UsersCollection();
			TasksCollection tasks = new TasksCollection();

			Console.WriteLine("No users or tasks added");
			Console.WriteLine("Users: " + users.Count());
			Console.WriteLine("Tasks: " + tasks.Count());
			XmlSerializer userXmlFormat = new XmlSerializer(typeof(UsersCollection));
			XmlSerializer taskXmlFormat = new XmlSerializer(typeof(TasksCollection));

            bool authSuccess = false;

			if (File.Exists("users.xml"))
			{
				try
				{
					using (FileStream fStream = new FileStream("users.xml", FileMode.Open))     // заменяет fStream.Close() и fStream.Dispose()
					{
						users = (UsersCollection)userXmlFormat.Deserialize(fStream);
					}
					Console.WriteLine("users.xml exists");
				}
				catch
				{
					Console.WriteLine("something went wrong 1");
				}
			}
            else
	        {
                users.Add("admin","admin");
                using(FileStream fStream = new FileStream("users.xml", FileMode.OpenOrCreate))
			    {
			    	userXmlFormat.Serialize(fStream, users);
			    }
                Console.WriteLine("users.xml created");
	        }

			Console.WriteLine("Users: " + users.Count());

			User current = new User();
			Authentication auth = new Authentication();
			
            while(!authSuccess)
            {
                try
                {
                    current = auth.dataInput();
                    if(users.Find(current))
                    {
                        Console.WriteLine("User found, login successful.");
                        authSuccess = true;
                    }
                    else
                    {
                        Console.WriteLine("User not found.");
                    }

                }
                catch
                {
                    Console.WriteLine("How the hell did you manage that?!");
                }


                //try                                                                                    //
			    //{                                                                                      //
			    //	current = users.Find(auth.dataInput());                                              //
			    //	if (current == null)                                                                 //
			    //	{                                                                                    //
			    //		current = users.Find(auth.dataInput());                                          //
                //                                                                                       //
                //        if (true)                                                                      //
	            //        {                                                                              //
                //                                                                                       //
	            //        }                                                                              // TODO: хрень, перед релизом удалить
                //                                                                                       //
			    //		Console.WriteLine("User not found!");			    	                         //
                //    }                                                                                  //
			    //	else                                                                                 //
			    //	{                                                                                    //
			    //		Console.WriteLine("login successful");                                           //
			    //		Console.WriteLine(current.info());                                               //
                //        authSuccess = true;                                                            //
			    //	}                                                                                    //
			    //	                                                                                     //
			    //}                                                                                      //
			    //catch                                                                                  //
			    //{                                                                                      //
			    //	Console.WriteLine("something went wrong 2");                                         //
			    //}
            }
            
            Console.WriteLine(current.info());
			Console.WriteLine("Pess any key to continue");
			Console.ReadKey();
			Console.Clear();
			//string theme = "t1";
			//string ques = "q1";
			//string[]ans = { "a1", "a2", "a3", "a4" };
			//int cor = 3;

			//Task t1 = new Task(theme, ques, ans, cor);
			//tasks.Add(t1);

			
			//using(FileStream fStream = new FileStream("tasks.xml", FileMode.OpenOrCreate))
			//{
			//	taskXmlFormat.Serialize(fStream, tasks);
			//}

			if (File.Exists("tasks.xml"))
			{
				try
				{
					using (FileStream fStream = new FileStream("tasks.xml", FileMode.Open))     // заменяет fStream.Close() и fStream.Dispose()
					{
						tasks = (TasksCollection)taskXmlFormat.Deserialize(fStream);
					}
					Console.WriteLine("tasks.xml exists");
				}
				catch
				{
					Console.WriteLine("tasks.xml is missing");
				}
			}
            else
            {
                string[] answrs = { "q1a1", "q1a2", "q1a3 correct", "q1a4" };
                Task testTask = new Task("testTheme", "testQuestionText", answrs, 3);
                tasks.Add(testTask);

                using (FileStream fStream = new FileStream("tasks.xml", FileMode.Create))
                {
                    taskXmlFormat.Serialize(fStream, tasks);

                }
                Console.WriteLine("tasks.xml created");

            }

            Console.WriteLine("Quiz start:");

            tasks.Run(current);

            Console.WriteLine(current.info());
			Console.ReadKey();
		}
	}
}

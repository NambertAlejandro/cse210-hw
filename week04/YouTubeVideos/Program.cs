using System;
using System.Collections.Generic;

namespace YouTubeVideos
{
    public static class Program
    {
        public static void Main()
        {
            List<Video> videos = new List<Video>();

            Video video1 = new Video("C# Abstraction in 10 Minutes", "CodeLab", 600);
            video1.AddComment(new Comment("Maria", "This made abstraction click for me."));
            video1.AddComment(new Comment("John", "Clear explanation and good examples."));
            video1.AddComment(new Comment("Lina", "Please do one on interfaces next."));
            videos.Add(video1);

            Video video2 = new Video("Encapsulation Explained", "DevWorld", 780);
            video2.AddComment(new Comment("Carlos", "Great breakdown of private fields."));
            video2.AddComment(new Comment("Ana", "Now I understand why getters/setters matter."));
            video2.AddComment(new Comment("Nate", "The analogy was perfect."));
            videos.Add(video2);

            Video video3 = new Video("OOP Composition: Has-a vs Is-a", "DotNetPro", 540);
            video3.AddComment(new Comment("Sofia", "Composition finally makes sense."));
            video3.AddComment(new Comment("Peter", "Nice examples with real objects."));
            video3.AddComment(new Comment("Kira", "Short and super useful."));
            videos.Add(video3);

            foreach (Video video in videos)
            {
                Console.WriteLine(video.GetTitle());
                Console.WriteLine(video.GetAuthor());
                Console.WriteLine(video.GetLengthSeconds());
                Console.WriteLine(video.GetNumberOfComments());

                foreach (Comment comment in video.GetComments())
                {
                    Console.WriteLine($"{comment.GetCommenterName()}: {comment.GetText()}");
                }

                Console.WriteLine();
            }
        }
    }
}

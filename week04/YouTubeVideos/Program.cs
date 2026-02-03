using System;
using System.Collections.Generic;
using System.Linq;

namespace YouTubeVideos
{
    public static class Program
    {
        public static void Main()
        {
            var app = new VideoApp(
                new Playlist("My Playlist"),
                new YouTubePlayer(),
                new YouTubeCatalog()
            );

            app.Run();
        }
    }

    public class VideoApp
    {
        private readonly Playlist _playlist;
        private readonly IVideoPlayer _player;
        private readonly YouTubeCatalog _catalog;

        public VideoApp(Playlist playlist, IVideoPlayer player, YouTubeCatalog catalog)
        {
            _playlist = playlist;
            _player = player;
            _catalog = catalog;

            _catalog.SeedDefault();
            foreach (var v in _catalog.GetTrending())
                _playlist.Add(v);
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("YouTube Videos");
                Console.WriteLine($"Playlist: {_playlist.Name}");
                Console.WriteLine($"Player status: {_player.GetStatus()}");
                Console.WriteLine();
                Console.WriteLine("1 - List videos");
                Console.WriteLine("2 - Play video");
                Console.WriteLine("3 - Pause");
                Console.WriteLine("4 - Stop");
                Console.WriteLine("5 - Like current video");
                Console.WriteLine("6 - Mark current as watched");
                Console.WriteLine("7 - Search catalog");
                Console.WriteLine("8 - Add video by ID");
                Console.WriteLine("9 - Remove video by ID");
                Console.WriteLine("0 - Exit");

                int choice = ReadInt(0, 9);

                switch (choice)
                {
                    case 1: ListVideos(); break;
                    case 2: PlayVideo(); break;
                    case 3: _player.Pause(); break;
                    case 4: _player.Stop(); break;
                    case 5: LikeCurrent(); break;
                    case 6: MarkWatched(); break;
                    case 7: SearchCatalog(); break;
                    case 8: AddById(); break;
                    case 9: RemoveById(); break;
                    case 0: return;
                }
            }
        }

        private void ListVideos()
        {
            Console.Clear();
            var videos = _playlist.GetAll();
            for (int i = 0; i < videos.Count; i++)
                Console.WriteLine($"{i} - {videos[i].GetSummary()}");
            Console.ReadKey();
        }

        private void PlayVideo()
        {
            var videos = _playlist.GetAll();
            if (videos.Count == 0) return;

            int index = ReadInt(0, videos.Count - 1);
            _player.Load(videos[index]);
            _player.Play();
            Console.ReadKey();
        }

        private void LikeCurrent()
        {
            var v = _player.GetCurrent();
            if (v != null) v.Like();
            Console.ReadKey();
        }

        private void MarkWatched()
        {
            var v = _player.GetCurrent();
            if (v != null) v.MarkWatched();
            Console.ReadKey();
        }

        private void SearchCatalog()
        {
            Console.Clear();
            Console.Write("Search term: ");
            string term = Console.ReadLine() ?? "";
            foreach (var v in _catalog.Search(term))
                Console.WriteLine(v.GetSummary());
            Console.ReadKey();
        }

        private void AddById()
        {
            Console.Write("Video ID: ");
            string id = Console.ReadLine() ?? "";
            var v = _catalog.FindById(id);
            if (v != null) _playlist.Add(v);
            Console.ReadKey();
        }

        private void RemoveById()
        {
            Console.Write("Video ID: ");
            string id = Console.ReadLine() ?? "";
            _playlist.Remove(id);
            Console.ReadKey();
        }

        private static int ReadInt(int min, int max)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int n) && n >= min && n <= max)
                    return n;
            }
        }
    }

    public interface IVideoPlayer
    {
        void Load(Video video);
        void Play();
        void Pause();
        void Stop();
        string GetStatus();
        Video? GetCurrent();
    }

    public class YouTubePlayer : IVideoPlayer
    {
        private Video? _current;
        private bool _playing;

        public void Load(Video video)
        {
            _current = video;
            _playing = false;
        }

        public void Play()
        {
            if (_current == null) return;
            _playing = true;
            _current.AddView();
        }

        public void Pause() => _playing = false;

        public void Stop()
        {
            _playing = false;
            _current = null;
        }

        public string GetStatus() => _playing ? "Playing" : "Stopped";

        public Video? GetCurrent() => _current;
    }

    public class Playlist
    {
        public string Name { get; }
        private readonly List<Video> _videos = new();

        public Playlist(string name) => Name = name;

        public void Add(Video video) => _videos.Add(video);

        public bool Remove(string id)
        {
            var v = _videos.FirstOrDefault(x => x.Id == id);
            if (v == null) return false;
            _videos.Remove(v);
            return true;
        }

        public List<Video> GetAll() => _videos;

        public Video GetByIndex(int index) => _videos[index];

        public int Count() => _videos.Count;
    }

    public class Video
    {
        public string Id { get; }
        private string _title;
        private string _channel;
        private TimeSpan _duration;
        private int _likes;
        private int _views;
        private bool _watched;

        public Video(string id, string title, string channel, TimeSpan duration)
        {
            Id = id;
            _title = title;
            _channel = channel;
            _duration = duration;
        }

        public void Like() => _likes++;

        public void AddView() => _views++;

        public void MarkWatched() => _watched = true;

        public string GetSummary()
            => $"{_title} | {_channel} | {_duration:mm\\:ss} | Likes:{_likes} Views:{_views}";
    }

    public class YouTubeCatalog
    {
        private readonly List<Video> _videos = new();

        public void SeedDefault()
        {
            _videos.Add(new Video("v1", "C# Basics", "CodeLab", TimeSpan.FromMinutes(10)));
            _videos.Add(new Video("v2", "OOP Explained", "DevWorld", TimeSpan.FromMinutes(15)));
            _videos.Add(new Video("v3", "Abstraction in C#", "DotNetPro", TimeSpan.FromMinutes(8)));
        }

        public List<Video> GetTrending() => _videos.Take(2).ToList();

        public Video? FindById(string id) => _videos.FirstOrDefault(v => v.Id == id);

        public List<Video> Search(string term)
            => _videos.Where(v => v.GetSummary().ToLower().Contains(term.ToLower())).ToList();
    }
}

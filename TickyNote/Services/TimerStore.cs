using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using TickyNote.Models;

namespace TickyNote.Services
{
    public class TimerStore
    {
        private readonly string _path;
        public TimerStore()
        {
            _path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TickyNote",
                "timers.json");
        }

        public List<TimerItem> Load()
        {
            if (!File.Exists(_path)) return new();
            try
            {
                return JsonSerializer.Deserialize<List<TimerItem>>(File.ReadAllText(_path)) ?? new();
            }
            catch
            {
                return new();
            }
        }

        public void Save(IEnumerable<TimerItem> items)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_path)!);
            File.WriteAllText(_path, JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}

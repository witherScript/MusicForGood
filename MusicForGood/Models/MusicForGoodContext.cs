using Microsoft.EntityFrameworkCore;

namespace MusicForGood.Models
{
  public class MusicForGoodContext : DbContext
  {
    public DbSet<Song> Songs { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<PlaylistSong> PlaylistsSongs { get; set; }

    public MusicForGoodContext(DbContextOptions options) : base(options) { }
  }
}
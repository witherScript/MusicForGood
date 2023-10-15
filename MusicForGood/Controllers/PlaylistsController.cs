using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MusicForGood.Models;
using System.Collections.Generic;
using System.Linq;

namespace MusicForGood.Controllers
{
  public class PlaylistsController : Controller 
  {
    private readonly MusicForGoodContext _db;
    public PlaylistsController(MusicForGoodContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Playlist> model = _db.Playlists.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Playlist playlist)
    {
      _db.Playlists.Add(playlist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Show(int id)
    {
      Playlist thisPlaylist = _db.Playlists
                              .Include(Playlist => Playlist.JoinEntities)
                              .ThenInclude(join => join.Song)
                              .FirstOrDefault(playlist => playlist.PlaylistId == id);
      return View(thisPlaylist);
    }

      public ActionResult AddSong(int id)
    {
      Playlist thisPlaylist = _db.Playlists.FirstOrDefault(playlist=> playlist.PlaylistId == id);
      ViewBag.SongId = new SelectList(_db.Songs, "SongId", "Title");
      return View(thisPlaylist);
    }

    [HttpPost]
    public ActionResult AddSong(Song song, int playlistId)
    {
      #nullable enable
      PlaylistSong? joinEntity = _db.PlaylistsSongs.FirstOrDefault(join => (join.SongId == song.SongId && join.PlaylistId == playlistId));
      #nullable disable
      if(joinEntity == null && playlistId != 0)
      {
        _db.PlaylistsSongs.Add(new PlaylistSong() { SongId = song.SongId, PlaylistId = playlistId });
        _db.SaveChanges();
      }
      return RedirectToAction("Show", new { id = playlistId });
    }

    public ActionResult Delete(int id)
    {
      Playlist thisPlaylist = _db.Playlists.FirstOrDefault(playlists => playlists.PlaylistId == id);
      return View(thisPlaylist);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Playlist thisPlaylist = _db.Playlists.FirstOrDefault(playlists => playlists.PlaylistId == id);
      _db.Playlists.Remove(thisPlaylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
 }
}


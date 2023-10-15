using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MusicForGood.Models;
using System.Collections.Generic;
using System.Linq;

namespace MusicForGood.Controllers
{
 public class SongsController: Controller
 {
  private readonly MusicForGoodContext _db;

  public SongsController(MusicForGoodContext db)
  {
    _db = db;
  }

  public ActionResult Index()
  {
    List<Song> model = _db.Songs.ToList();
    return View(model);
  }
  
  public ActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public ActionResult Create(Song song)
  {
    if (!ModelState.IsValid)
    {
      return View(song);
    }
    else
    {
      _db.Songs.Add(song);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }

    public ActionResult Show(int id)
  {
    Song thisSong = _db.Songs
                             .Include(song => song.JoinEntities)
                             .ThenInclude(join => join.Playlist)
                             .FirstOrDefault(song => song.SongId == id);
    return View(thisSong);
  }

  public ActionResult AddPlaylist(int id)
    {
      Song thisSong = _db.Songs.FirstOrDefault(song => song.SongId == id);
      ViewBag.PlaylistId = new SelectList(_db.Playlists, "PlaylistId", "Name");
      return View(thisSong);
    }

  [HttpPost]
  public ActionResult AddPlaylist(Playlist playlist, int songId)
  {
    #nullable enable
    PlaylistSong? joinEntity = _db.PlaylistsSongs.FirstOrDefault(join => (join.PlaylistId == playlist.PlaylistId && join.SongId == songId));
    #nullable disable
    if(joinEntity == null && songId != 0)
    {
      _db.PlaylistsSongs.Add(new PlaylistSong() { PlaylistId = playlist.PlaylistId, SongId = songId });
      _db.SaveChanges();
    }
    return RedirectToAction("Show", new { id = songId });
  }

    public ActionResult Delete(int id)
    {
      Song thisSong = _db.Songs.FirstOrDefault(songs => songs.SongId == id);
      return View(thisSong);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Song thisSong = _db.Songs.FirstOrDefault(songs => songs.SongId == id);
      _db.Songs.Remove(thisSong);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
 }
}

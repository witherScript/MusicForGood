using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace MusicForGood.Models;
public class Playlist
{
  [Required(ErrorMessage="Name cannot be empty")]
  public string Name {get; set;}
  public int PlaylistId {get; set;}
  public List<PlaylistSong> JoinEntities{get; set;}
}

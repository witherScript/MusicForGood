using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace MusicForGood.Models;
public class Song
{
  public int SongId {get; set;}
  
  [Required(ErrorMessage="Title cannot be empty")]
  public string Title {get; set;}

  List<PlaylistSong> JoinEntities {get; set;}
}

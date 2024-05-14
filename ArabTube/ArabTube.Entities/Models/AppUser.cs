﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ArabTube.Entities.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Videos = new List<Video>();
            History = new List<WatchedVideo>();
            WatchedVideos = new List<Video>();
            Followers = new List<AppUserConnection>();
            Following = new List<AppUserConnection>();
            Playlists = new List<Playlist>();
            Comments = new List<Comment>();
        }

        [Required, MaxLength(250)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(250)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public bool Gender { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Video> Videos { get; set; }

        public virtual ICollection<Video> WatchedVideos { get; set; }
        public virtual ICollection<WatchedVideo> History { get; set; }

        public virtual ICollection<AppUserConnection> Followers { get; set; } 
        public virtual ICollection<AppUserConnection> Following { get; set; } 

        public virtual ICollection<Playlist> Playlists { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}

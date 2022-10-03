using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCRCaculator.Update
{
    public class GitHubRelease
    {
        private readonly string _owner;
        private readonly string _repo;

        public GitHubRelease(string owner, string repo)
        {
            _owner = owner;
            _repo = repo;
        }

        public string AllReleaseUrl => $@"https://api.github.com/repos/{_owner}/{_repo}/releases";

    }
    public class Release
    {
        public string url { get; set; }

        public string assets_url { get; set; }

        public string upload_url { get; set; }

        public string html_url { get; set; }

        public int id { get; set; }

        public string node_id { get; set; }

        public string tag_name { get; set; }

        public string target_commitish { get; set; }

        public string name { get; set; }

        public bool draft { get; set; }

        public GitHubUser author { get; set; }

        public bool prerelease { get; set; }

        public DateTime created_at { get; set; }

        public DateTime published_at { get; set; }

        public Asset[] assets { get; set; }

        public string tarball_url { get; set; }

        public string zipball_url { get; set; }

        public string body { get; set; }
    }
    public class GitHubUser
    {
        public string login { get; set; }

        public int id { get; set; }

        public string node_id { get; set; }

        public string avatar_url { get; set; }

        public string gravatar_id { get; set; }

        public string url { get; set; }

        public string html_url { get; set; }

        public string followers_url { get; set; }

        public string following_url { get; set; }

        public string gists_url { get; set; }

        public string starred_url { get; set; }

        public string subscriptions_url { get; set; }

        public string organizations_url { get; set; }

        public string repos_url { get; set; }

        public string events_url { get; set; }

        public string received_events_url { get; set; }

        public string type { get; set; }

        public bool site_admin { get; set; }
    }
    public class Asset
    {
        public string url { get; set; }

        public int id { get; set; }

        public string node_id { get; set; }

        public string name { get; set; }

        public object label { get; set; }

        public GitHubUser uploader { get; set; }

        public string content_type { get; set; }

        public string state { get; set; }

        public int size { get; set; }

        public int download_count { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public string browser_download_url { get; set; }
    }

}

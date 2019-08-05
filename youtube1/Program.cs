
using System;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using System.Collections.Generic;

namespace YoutubeVideos
{
    class Program
    {
        static YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer() { ApiKey = "YOUR_API_KEY" });
        static int maxComment = 30,maxVideo=40,days=-1000;
        static void Main(string[] args)
        {
            string channelId = "CHANNEL_ID";
            string videoId = "VIDEO_ID";
            var url = "https://www.youtube.com/c/EUinTurkey";
            var response = getAllCommentsOfAVideo(channelId);
            Console.ReadLine();
        }


        //getComments of a channel -- get all comments of channel(all pages,it takes too much time)
        private static Google.Apis.YouTube.v3.Data.CommentThreadListResponse getCommentsOfAChannel(String channelId)
        {
            var request = youtubeService.CommentThreads.List("snippet");
            request.AllThreadsRelatedToChannelId = channelId;
            request.Order = CommentThreadsResource.ListRequest.OrderEnum.Time;
            request.MaxResults = maxComment;
            var response = request.Execute();
            return response;
        }
        private static List<Google.Apis.YouTube.v3.Data.CommentThreadListResponse> getAllCommentsOfAChannel(String channelId)
        {
            var request = youtubeService.CommentThreads.List("snippet");
            request.AllThreadsRelatedToChannelId = channelId;
            request.Order = CommentThreadsResource.ListRequest.OrderEnum.Time;
            request.MaxResults = maxComment;
            List<Google.Apis.YouTube.v3.Data.CommentThreadListResponse> response = new List<Google.Apis.YouTube.v3.Data.CommentThreadListResponse>();

            response.Add(request.Execute());

            for (int i = 0; response[i].NextPageToken != null; i++)
            {
                request.PageToken = ((string)response[i].NextPageToken);
                response.Add(request.Execute());
            }
            return response;
        }
        //

        //get comments of a video -- get all comments of a video(all pages)
        private static Google.Apis.YouTube.v3.Data.CommentThreadListResponse getCommentsOfAVideo(String videoId)
        {
            var request = youtubeService.CommentThreads.List("snippet");
            request.VideoId = videoId;
            request.Order = CommentThreadsResource.ListRequest.OrderEnum.Time;
            request.MaxResults = maxComment;
            var response = request.Execute();
            return response;
        }
        private static List<Google.Apis.YouTube.v3.Data.CommentThreadListResponse> getAllCommentsOfAVideo(String videoId)
        {
            var request = youtubeService.CommentThreads.List("snippet");
            request.VideoId = videoId;
            request.Order = CommentThreadsResource.ListRequest.OrderEnum.Time;
            request.MaxResults = maxComment;
            List<Google.Apis.YouTube.v3.Data.CommentThreadListResponse> response = new List<Google.Apis.YouTube.v3.Data.CommentThreadListResponse>();

            response.Add(request.Execute());

            for (int i = 0; response[i].NextPageToken != null; i++)
            {
                request.PageToken = ((string)response[i].NextPageToken);
                response.Add(request.Execute());
            }
            return response;
        }
        //



        //get videos by a word -- get all videos by a word(all pages)
        private static Google.Apis.YouTube.v3.Data.SearchListResponse getVideosByWord(String word)
        {
            DateTime publishAfter = DateTime.Now;
            publishAfter = publishAfter.AddDays(days);//Son x gündeki videolar


            var request = youtubeService.Search.List("snippet");
            request.Q =word;
            request.Order = SearchResource.ListRequest.OrderEnum.Date;
            request.Type = "channel";
            var response = request.Execute();
            request.MaxResults = 30;
            request.PublishedAfter = publishAfter;
            return response;
        }
        private static List<Google.Apis.YouTube.v3.Data.SearchListResponse> getAllVideosByWord(String word)
        {
            List<Google.Apis.YouTube.v3.Data.SearchListResponse> response = new List<Google.Apis.YouTube.v3.Data.SearchListResponse>();
            DateTime publishAfter = DateTime.Now;
            publishAfter = publishAfter.AddDays(days);//Son x gündeki videolar
            var request = youtubeService.Search.List("snippet");
            request.Q = word;
            request.Order = SearchResource.ListRequest.OrderEnum.Date;
            request.Type = "channel";
            request.PublishedAfter = publishAfter;
            response.Add(request.Execute());

            for (int i = 0; response[i].NextPageToken != null; i++)
            {
                request.PublishedAfter = publishAfter;
                response.Add(request.Execute());
            }
            return response;
        }

        //get videos of a channel -- get all videos of a channel(all pages)
        private static Google.Apis.YouTube.v3.Data.SearchListResponse getVideosOfAChannel(String channelId)
        {
            DateTime publishAfter = DateTime.Now;
            publishAfter = publishAfter.AddDays(days);//Son x gündeki videolar
            var request = youtubeService.Search.List("snippet");
            request.ChannelId = channelId;//Kanalın Id'si
            request.MaxResults = maxVideo;//Video sayısı(max 50)
            request.PublishedAfter = publishAfter;


            var response = request.Execute();
            return response;
        }
        private static List<Google.Apis.YouTube.v3.Data.SearchListResponse> getAllVideosOfAChannel(String channelId)
        {
            List<Google.Apis.YouTube.v3.Data.SearchListResponse> response = new List<Google.Apis.YouTube.v3.Data.SearchListResponse>();
            DateTime publishAfter = DateTime.Now;
            publishAfter = publishAfter.AddDays(days);//After x days
            var request = youtubeService.Search.List("snippet");
            request.ChannelId = channelId;//
            request.MaxResults = maxVideo;//Max 50
            request.PublishedAfter = publishAfter;
            response.Add(request.Execute());

            for (int i = 0; response[i].NextPageToken != null; i++)
            {
                request.PageToken = ((string)response[i].NextPageToken);
                response.Add(request.Execute());
            }
            return response;
        }
        //



        //get subscribed channels by a channel -- getall subscribed channels by a channel(all pages)
        private static Google.Apis.YouTube.v3.Data.SubscriptionListResponse getSubscribedChannels(String channelId)
        {
            var request = youtubeService.Subscriptions.List("snippet");
            request.ChannelId = "UCCgT8h4gXbaLEiUk80T8k_A";
            request.MaxResults = 30;
            var response = request.Execute();
            return response;
        }
        private static List<Google.Apis.YouTube.v3.Data.SubscriptionListResponse> getAllSubscribedChannels(String channelId)
        {
            List<Google.Apis.YouTube.v3.Data.SubscriptionListResponse> response = new List<Google.Apis.YouTube.v3.Data.SubscriptionListResponse>();

            var request = youtubeService.Subscriptions.List("snippet");
            request.ChannelId = "UCCgT8h4gXbaLEiUk80T8k_A";
            request.MaxResults = 30;
            response.Add(request.Execute());


            for (int i = 0; response[i].NextPageToken != null; i++)
            {
                request.PageToken = ((string)response[i].NextPageToken);
                response.Add(request.Execute());
            }

            return response;
        }
        //



        //get statistics of a channel
        private static Google.Apis.YouTube.v3.Data.ChannelListResponse getChannelsStatistics(String channelId)
        {
            var request = youtubeService.Channels.List("statistics");   
            request.Id = channelId;
            var response = request.Execute();
            return response;
        }
        //

        //get statistics of a video
        private static Google.Apis.YouTube.v3.Data.VideoListResponse getVideosStatistics(String videoId)
        {
            var request = youtubeService.Videos.List("statistics");
            request.Id = videoId;
            var response = request.Execute();
            return response;
        }
        //




        // find channel id from url
        private static string findIdFromUrl(string url)
        {
            string channelId;
            if (url.Contains(".com/user/"))
            {
                channelId = findUnknownId(url);
                channelId = findIdWithUsername(channelId);

            }
            else if (url.Contains(".com/channel/"))
            {
                channelId = findUnknownId(url);
            }
            else if (url.Contains(".com/c/"))
            {
                channelId = findUnknownId(url);

                channelId = findIdWithCustomname(channelId);
            }
            else
            {
                url = url.Insert(7, "/");
                string unknown = findUnknownId(url);
                channelId = findIdWithUsername(unknown);
                if (channelId == null)
                {
                    channelId = findIdWithCustomname(unknown);
                }
            }
            return channelId;
        }
        //
        //submethods of findIdFromUrl
        private static string findUnknownId(string url)
        {
            int counter1 = 2, counter2, i = 0; 

            if (url.StartsWith('h')) 
            {
                counter1 = 4;
            }
            while (counter1 > 0)
            {
                if (url[i] == '/')
                {
                    counter1--;
                }
                if(url.Length-1 == i  && counter1>0)
                {
                    return null;
                }
                i++;
            }
            counter2 = i;
            while (true)
            {
                if ((url.Length - 1) == counter2)
                {
                    return url.Substring(i, counter2 - i+1);
                }
                else if(counter2 == url.Length)
                {
                    return null;
                }

                counter2++;
                if (url[counter2] == '/')
                {
                    return url.Substring(i, counter2 - i);
                }

            }
        }
        private static string findIdWithUsername(string username)
        {

            var channelsListRequest = youtubeService.Channels.List("snippet");
            channelsListRequest.ForUsername = username;//User Id'si
            var channelResponse = channelsListRequest.Execute();
            if (channelResponse.Items.Count == 0)
            {
                return null;
            }
            username = channelResponse.Items[0].Id;

            return username;
        }
        private static string findIdWithCustomname(string customName)
        {
            var request = youtubeService.Search.List("snippet");
            request.Type = "channel";
            request.Q = customName;
            var response = request.Execute();
            if (response.Items.Count == 0)
            {
                return null;
            }
            return response.Items[0].Id.ChannelId;
        }
        //
    }
}




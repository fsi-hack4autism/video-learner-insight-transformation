using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace VideoAnalysis.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Block
    {
        public int id { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class Brand
    {
        public int id { get; set; }
        public string referenceType { get; set; }
        public string name { get; set; }
        public string referenceId { get; set; }
        public string referenceUrl { get; set; }
        public string description { get; set; }
        public List<object> tags { get; set; }
        public double confidence { get; set; }
        public bool isCustom { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class Emotion
    {
        public int id { get; set; }
        public string type { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class Face
    {
        public int id { get; set; }
        public string name { get; set; }
        public int confidence { get; set; }
        public object description { get; set; }
        public string thumbnailId { get; set; }
        public object title { get; set; }
        public object imageUrl { get; set; }
        public List<Thumbnail> thumbnails { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class Insights
    {
        public string version { get; set; }
        public string duration { get; set; }
        public string sourceLanguage { get; set; }
        public List<string> sourceLanguages { get; set; }
        public string language { get; set; }
        public List<string> languages { get; set; }
        public List<Transcript> transcript { get; set; }
        public List<Keyword> keywords { get; set; }
        public List<Topic> topics { get; set; }
        public List<Face> faces { get; set; }
        public List<Label> labels { get; set; }
        public List<Scene> scenes { get; set; }
        public List<Shot> shots { get; set; }
        public List<Brand> brands { get; set; }
        public List<NamedLocation> namedLocations { get; set; }
        public List<NamedPerson> namedPeople { get; set; }
        public List<Sentiment> sentiments { get; set; }
        public List<Emotion> emotions { get; set; }
        public List<VisualContentModeration> visualContentModeration { get; set; }
        public List<Block> blocks { get; set; }
        public List<Speaker> speakers { get; set; }
        public TextualContentModeration textualContentModeration { get; set; }
        public Statistics statistics { get; set; }
    }

    public class Instance
    {
        public string adjustedStart { get; set; }
        public string adjustedEnd { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string brandType { get; set; }
        public string instanceSource { get; set; }
        public double confidence { get; set; }
        public List<string> thumbnailsIds { get; set; }
        public string thumbnailId { get; set; }
    }

    public class KeyFrame
    {
        public int id { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class Keyword
    {
        public int id { get; set; }
        public string text { get; set; }
        public double confidence { get; set; }
        public string language { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class Label
    {
        public int id { get; set; }
        public string name { get; set; }
        public string language { get; set; }
        public List<Instance> instances { get; set; }
        public string referenceId { get; set; }
    }

    public class NamedLocation
    {
        public int id { get; set; }
        public string name { get; set; }
        public string referenceId { get; set; }
        public string referenceUrl { get; set; }
        public string description { get; set; }
        public List<object> tags { get; set; }
        public double confidence { get; set; }
        public bool isCustom { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class NamedPerson
    {
        public int id { get; set; }
        public string name { get; set; }
        public object referenceId { get; set; }
        public object referenceUrl { get; set; }
        public object description { get; set; }
        public List<object> tags { get; set; }
        public double confidence { get; set; }
        public bool isCustom { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class Range
    {
        public string start { get; set; }
        public string end { get; set; }
    }

    public class VideoModel
    {
        public object partition { get; set; }
        public object description { get; set; }
        public string privacyMode { get; set; }
        public string state { get; set; }
        public string accountId { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string userName { get; set; }
        public DateTime created { get; set; }
        public bool isOwned { get; set; }
        public bool isEditable { get; set; }
        public bool isBase { get; set; }
        public int durationInSeconds { get; set; }
        public string duration { get; set; }
        public object summarizedInsights { get; set; }
        public List<Video> videos { get; set; }
        public List<VideosRange> videosRanges { get; set; }
    }

    public class VideoModelInfo : VideoModel, INotifyPropertyChanged
    {
        private VideoModel _baseInstance;
        public VideoModelInfo(VideoModel baseInstance)
        {
            _baseInstance = baseInstance;
            SpeakerInfos = new List<SpeakerInfo>();

            var video = _baseInstance.videos.First();
            if (video != null)
            {
                var speakers = video.insights?.speakers;
                foreach (var speaker in speakers)
                {
                    SpeakerInfos.Add(new SpeakerInfo(speaker));
                }
            }
        }

        public string Name { get { return _baseInstance.name; } }
        public string UserName { get { return _baseInstance.userName; } }
        public DateTime Created { get { return _baseInstance.created; } }
        public string Duration { get { return _baseInstance.duration; } }
        public List<Video> Videos { get { return _baseInstance.videos; } }

        public Insights FirstVideoInsights { get { return _baseInstance.videos.First().insights; } }
        public List<SpeakerInfo> SpeakerInfos { get; set; }

        private bool _isAggregated;
        public bool IsAggregated
        {
            get { return _isAggregated; }
            set
            {
                if (_isAggregated != value)
                {
                    _isAggregated = value;
                    NotifyPropertyChange(nameof(IsAggregated));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChange(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class Scene
    {
        public int id { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class Sentiment
    {
        public int id { get; set; }
        public double averageScore { get; set; }
        public string sentimentType { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class Shot
    {
        public int id { get; set; }
        public List<string> tags { get; set; }
        public List<KeyFrame> keyFrames { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class Speaker
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class SpeakerInfo : Speaker, INotifyPropertyChanged
    {
        private Speaker _baseInstance;
        public SpeakerInfo(Speaker baseInstance)
        {
            _baseInstance = baseInstance;
        }

        public string Name
        {
            get { return _baseInstance.name; }
        }

        public int Id
        {
            get { return _baseInstance.id; }
        }

        private List<TranscriptInfo> _transcripts;
        public List<TranscriptInfo> Transcripts
        {
            get { return _transcripts; }
            set
            {
                if(_transcripts != value) 
                { 
                    _transcripts = value;
                    NotifyPropertyChange(nameof(Transcripts));
                }
            }
        }

        private int _transcriptCount;
        public int TranscriptCount
        {
            get { return _transcriptCount; }
            set
            {
                if (_transcriptCount != value)
                {
                    _transcriptCount = value;
                    NotifyPropertyChange(nameof(TranscriptCount));
                }
            }
        }

        private int _wordCount;
        public int WordCount
        {
            get { return _wordCount; }
            set
            {
                if (_wordCount != value)
                {
                    _wordCount = value;
                    NotifyPropertyChange(nameof(WordCount));
                }
            }
        }

        private int _grammerMistakes;
        public int GrammerMistakes
        {
            get { return _grammerMistakes; }
            set
            {
                if (_grammerMistakes != value)
                {
                    _grammerMistakes = value;
                    NotifyPropertyChange(nameof(GrammerMistakes));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChange(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class SpeakerLongestMonolog
    {
        [JsonProperty("1")]
        public int _1 { get; set; }

        [JsonProperty("2")]
        public int _2 { get; set; }

        [JsonProperty("3")]
        public int _3 { get; set; }

        [JsonProperty("4")]
        public int _4 { get; set; }

        [JsonProperty("5")]
        public int _5 { get; set; }
    }

    public class SpeakerNumberOfFragments
    {
        [JsonProperty("1")]
        public int _1 { get; set; }

        [JsonProperty("2")]
        public int _2 { get; set; }

        [JsonProperty("3")]
        public int _3 { get; set; }

        [JsonProperty("4")]
        public int _4 { get; set; }

        [JsonProperty("5")]
        public int _5 { get; set; }
    }

    public class SpeakerTalkToListenRatio
    {
        [JsonProperty("1")]
        public double _1 { get; set; }

        [JsonProperty("2")]
        public double _2 { get; set; }

        [JsonProperty("3")]
        public double _3 { get; set; }

        [JsonProperty("4")]
        public double _4 { get; set; }

        [JsonProperty("5")]
        public double _5 { get; set; }
    }

    public class SpeakerWordCount
    {
        [JsonProperty("1")]
        public int _1 { get; set; }

        [JsonProperty("2")]
        public int _2 { get; set; }

        [JsonProperty("3")]
        public int _3 { get; set; }

        [JsonProperty("4")]
        public int _4 { get; set; }

        [JsonProperty("5")]
        public int _5 { get; set; }
    }

    public class Statistics
    {
        public int correspondenceCount { get; set; }
        public SpeakerTalkToListenRatio speakerTalkToListenRatio { get; set; }
        public SpeakerLongestMonolog speakerLongestMonolog { get; set; }
        public SpeakerNumberOfFragments speakerNumberOfFragments { get; set; }
        public SpeakerWordCount speakerWordCount { get; set; }
    }

    public class TextualContentModeration
    {
        public int id { get; set; }
        public int bannedWordsCount { get; set; }
        public int bannedWordsRatio { get; set; }
        public List<object> instances { get; set; }
    }

    public class Thumbnail
    {
        public string id { get; set; }
        public string fileName { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class Topic
    {
        public int id { get; set; }
        public string name { get; set; }
        public string referenceId { get; set; }
        public string referenceType { get; set; }
        public double confidence { get; set; }
        public string iabName { get; set; }
        public string language { get; set; }
        public List<Instance> instances { get; set; }
        public string iptcName { get; set; }
    }

    public class Transcript
    {
        public int id { get; set; }
        public string text { get; set; }
        public double confidence { get; set; }
        public int speakerId { get; set; }
        public string language { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class TranscriptInfo : Transcript, INotifyPropertyChanged
    {
        private Transcript _baseInstance;
        public TranscriptInfo(Transcript baseInstance)
        {
            _baseInstance = baseInstance;

            char[] delimiters = new char[] { ' ', '\r', '\n' };
            WordCount = Text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public string Text { get { return _baseInstance.text; } }

        private int _wordCount;
        public int WordCount
        {
            get { return _wordCount; }
            set
            {
                if (_wordCount != value)
                {
                    _wordCount = value;
                    NotifyPropertyChange(nameof(WordCount));
                }
            }
        }

        private int _grammerMistakes;

        public int GrammerMistakes
        {
            get { return _grammerMistakes; }
            set
            {
                if (_grammerMistakes != value)
                {
                    _grammerMistakes = value;
                    NotifyPropertyChange(nameof(GrammerMistakes));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChange(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class Video
    {
        public string accountId { get; set; }
        public string id { get; set; }
        public string state { get; set; }
        public string moderationState { get; set; }
        public string reviewState { get; set; }
        public string privacyMode { get; set; }
        public string processingProgress { get; set; }
        public string failureCode { get; set; }
        public string failureMessage { get; set; }
        public object externalId { get; set; }
        public object externalUrl { get; set; }
        public object metadata { get; set; }
        public Insights insights { get; set; }
        public string thumbnailId { get; set; }
        public bool detectSourceLanguage { get; set; }
        public string languageAutoDetectMode { get; set; }
        public string sourceLanguage { get; set; }
        public List<string> sourceLanguages { get; set; }
        public string language { get; set; }
        public List<string> languages { get; set; }
        public string indexingPreset { get; set; }
        public string linguisticModelId { get; set; }
        public string personModelId { get; set; }
        public object logoGroupId { get; set; }
        public bool isAdult { get; set; }
        public string publishedUrl { get; set; }
        public object publishedProxyUrl { get; set; }
        public object viewToken { get; set; }
    }

    public class VideosRange
    {
        public string videoId { get; set; }
        public Range range { get; set; }
    }

    public class VisualContentModeration
    {
        public int id { get; set; }
        public double adultScore { get; set; }
        public double racyScore { get; set; }
        public List<Instance> instances { get; set; }
    }


}

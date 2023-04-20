using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using VideoAnalysis.Models;
using VideoAnalysis.UI.Common;

namespace VideoAnalysis.DataView
{
    public enum MonthName
    {
        [Display(Name = "January")]
        January = 1,

        [Display(Name = "February")]
        February = 2,

        [Display(Name = "March")]
        March = 3,

        [Display(Name = "April")]
        April = 4,

        [Display(Name = "May")]
        May = 5,

        [Display(Name = "June")]
        June = 6,

        [Display(Name = "July")]
        July = 7,

        [Display(Name = "August")]
        August = 8,

        [Display(Name = "September")]
        September = 9,

        [Display(Name = "October")]
        October = 10,
        
        [Display(Name = "November")]
        November = 11,

        [Display(Name = "December")]
        December = 12
    }
    public class VideoDataViewModel : INotifyPropertyChanged
    {
        private string _patientName;
        public string PatientName 
        {
            get { return _patientName; }
            set 
            {
                if (_patientName != value)
                {
                    _patientName = value;
                    NotifyPropertyChange(nameof(PatientName));
                }
            }
        }


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

        private bool _isReset;
        public bool IsReset
        {
            get { return _isReset; }
            set
            {
                if (_isReset != value)
                {
                    _isReset = value;
                    NotifyPropertyChange(nameof(IsReset));
                }
            }
        }

        public ObservableCollection<VideoModelInfo> _videoModels;
        public ObservableCollection<VideoModelInfo> VideoModels
        {
            get { return _videoModels; }
            set
            {
                if (_videoModels != value)
                {
                    _videoModels = value;
                    NotifyPropertyChange(nameof(VideoModels));
                }
            }
        }

        private VideoModelInfo _selectedVideoModel;
        public VideoModelInfo SelectedVideoModel
        {
            get { return _selectedVideoModel; }
            set
            {
                if (_selectedVideoModel != value)
                {
                    _selectedVideoModel = value;
                    NotifyPropertyChange(nameof(SelectedVideoModel));
                }
            }

        }

        private SpeakerInfo _selectedSpeaker;
        public SpeakerInfo SelectedSpeaker
        {
            get { return _selectedSpeaker; }
            set
            {
                if (_selectedSpeaker != value)
                {
                    _selectedSpeaker = value;
                    NotifyPropertyChange(nameof(SelectedSpeaker));
                }
            }

        }

        public List<MonthName> _months;
        public List<MonthName> Months
        {
            get { return _months; }
            set
            {
                if (_months != value)
                {
                    _months = value;
                    NotifyPropertyChange(nameof(Months));
                }
            }
        }

        private MonthName _selectedMonth;
        public MonthName SelectedMonth 
        { 
            get { return _selectedMonth; }
            set
            {
                if (SelectedMonth != value)
                {
                    _selectedMonth = value;
                    NotifyPropertyChange(nameof(SelectedMonth));
                }
            }
        }

        public List<int> _years;
        public List<int> Years
        {
            get { return _years; }
            set
            {
                if (_years != value)
                {
                    _years = value;
                    NotifyPropertyChange(nameof(Years));
                }
            }
        }

        private int _selectedYear;
        public int SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    NotifyPropertyChange(nameof(SelectedYear));
                }
            }
        }

        private int _selectedMonthlyYear;
        public int SelectedMonthlyYear
        {
            get { return _selectedMonthlyYear; }
            set
            {
                if (_selectedMonthlyYear != value)
                {
                    _selectedMonthlyYear = value;
                    NotifyPropertyChange(nameof(SelectedMonthlyYear));

                    Months.Clear();
                    foreach (var videoModelInfo in VideoModels)
                    {
                        if (videoModelInfo.Created.Year.Equals(_selectedMonthlyYear))
                        {
                            if (!Months.Contains((MonthName)videoModelInfo.Created.Month))
                            {
                                Months.Add((MonthName)videoModelInfo.Created.Month);
                            }
                        }
                    }
                }
            }
        }

        private bool _filterYearly;
        public bool FilterYearly
        {
            get { return _filterYearly; }
            set
            {
                if (_filterYearly != value)
                {
                    _filterYearly = value;
                    NotifyPropertyChange(nameof(FilterYearly));
                }
            }
        }

        private bool _filterMonthly;
        public bool FilterMonthly
        {
            get { return _filterMonthly; }
            set
            {
                if (_filterMonthly != value)
                {
                    _filterMonthly = value;
                    NotifyPropertyChange(nameof(FilterMonthly));
                }
            }
        }

        public ICommand AggregateCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }

        public VideoDataViewModel()
        {
            
            Months = new List<MonthName>
            {
                MonthName.January, MonthName.February, MonthName.March,
                MonthName.April, MonthName.May, MonthName.June,
                MonthName.July, MonthName.August, MonthName.September,
                MonthName.October, MonthName.November, MonthName.December
            };

            Years = new List<int>();

            OnReset(null);

            AggregateCommand = new DelegateCommand(OnAggregate);
            ResetCommand = new DelegateCommand(OnReset);
        }

        private void OnAggregate(object commandParameters)
        {
            List<VideoModelInfo> filteredVideoModels = null;
            if (FilterYearly)
            {
                filteredVideoModels = GetYearlyVideos(SelectedYear);
            }
            else if (FilterMonthly)
            {
                filteredVideoModels = GetMonthlyVideos(SelectedMonthlyYear, (int)SelectedMonth);
            }

            foreach (var filterVideoModel in filteredVideoModels)
            {
                UpdateTranscriptInfo(filterVideoModel);
            }

            IsAggregated = true;
        }

        private void OnReset(object commandParameters)
        {
            VideoModels = new ObservableCollection<VideoModelInfo>();
            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var files = Directory.GetFiles($@"{directoryName}\JSON", "*.json");
            foreach (var file in files)
            {
                using (StreamReader jsonReader = new StreamReader(file))
                {
                    string jsonString = jsonReader.ReadToEnd();
                    var videoModel = Newtonsoft.Json.JsonConvert.DeserializeObject<VideoModel>(jsonString);
                    if (!Years.Contains(videoModel.created.Year))
                    {
                        Years.Add(videoModel.created.Year);
                    }

                    VideoModels.Add(new VideoModelInfo(videoModel));
                }
            }

            if (Years.Count > 0)
            {
                SelectedYear = Years.First();
                SelectedMonthlyYear = Years.First();
                SelectedMonth = Months.First();
            }

            IsReset = true;
        }

        private List<VideoModelInfo> GetYearlyVideos(int year)
        {
            var filteredVideoModels = new List<VideoModelInfo>();
            foreach (var videoModelInfo in VideoModels)
            {
                if(videoModelInfo.Created.Year.Equals(year))
                {
                    videoModelInfo.IsAggregated = true;
                    filteredVideoModels.Add(videoModelInfo);
                }
                else
                {
                    videoModelInfo.IsAggregated = false;
                }
            }

            return filteredVideoModels;
        }

        private List<VideoModelInfo> GetMonthlyVideos(int year, int month)
        {
            var filteredVideoModels = new List<VideoModelInfo>();
            foreach (var videoModelInfo in VideoModels)
            {
                if (videoModelInfo.Created.Year.Equals(year) && videoModelInfo.Created.Month.Equals(month))
                {
                    videoModelInfo.IsAggregated = true;
                    filteredVideoModels.Add(videoModelInfo);
                }
                else
                {
                    videoModelInfo.IsAggregated = false;
                }
            }

            return filteredVideoModels;
        }

        private void UpdateTranscriptInfo(VideoModelInfo videoModelInfo)
        {
            var speakers = videoModelInfo.SpeakerInfos;
            foreach (var speaker in speakers)
            {
                speaker.Transcripts = GetTranscripts(videoModelInfo, speaker);
                speaker.TranscriptCount = speaker.Transcripts.Count;
                
                //var combinedTranscripts = string.Join(" ", speaker.Transcripts.Select(tr => tr.text));
                //char[] delimiters = new char[] { ' ', '\r', '\n' };
                //speaker.WordCount = combinedTranscripts.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
                speaker.WordCount = speaker.Transcripts.Select(tr=>tr.WordCount).Sum();
            }
        }

        private List<TranscriptInfo> GetTranscripts(VideoModelInfo videoModelInfo, SpeakerInfo speaker)
        {
            return (from transcript in videoModelInfo.FirstVideoInsights.transcript
                    where transcript != null && transcript.speakerId.Equals(speaker.Id)
                    select new TranscriptInfo(transcript)).ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChange(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if(propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

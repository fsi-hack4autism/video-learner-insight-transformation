using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoAnalysis.DataView;
using VideoAnalysis.Models;

namespace VideoAnalysis.Aggregator
{
    public class VideoAggregationViewModel : INotifyPropertyChanged
    {
        private VideoDataViewModel _videoDataViewModel = null;
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

        public ObservableCollection<EmotionInfo> _emotionInfos;
        public ObservableCollection<EmotionInfo> EmotionInfos
        {
            get { return _emotionInfos; }
            set
            {
                if (_emotionInfos != value)
                {
                    _emotionInfos = value;
                    NotifyPropertyChange(nameof(EmotionInfos));
                }
            }
        }

        public EmotionInfo _selectedEmotionInfo;
        public EmotionInfo SelectedEmotionInfo
        {
            get { return _selectedEmotionInfo; }
            set
            {
                if (_selectedEmotionInfo != value)
                {
                    _selectedEmotionInfo = value;
                    NotifyPropertyChange(nameof(SelectedEmotionInfo));
                }
            }
        }

        public VideoAggregationViewModel(VideoDataViewModel videoDataViewModel)
        {
            _videoDataViewModel = videoDataViewModel;
            _videoDataViewModel.PropertyChanged += VideoDataViewModel_PropertyChanged;
        }

        private void VideoDataViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(VideoDataViewModel.IsAggregated)))
            {
                VideoModels = new ObservableCollection<VideoModelInfo>(_videoDataViewModel.VideoModels.Where(vm => vm.IsAggregated).ToList());
                EmotionInfos = new ObservableCollection<EmotionInfo>();
                foreach (var videoModel in VideoModels)
                {
                    var emotionInfos = GetEmotionInfos(videoModel);
                    foreach (var emInfo in emotionInfos)
                    {
                        if (EmotionInfos.Any(em => em.Type.Equals(emInfo.Type) && em.CreatedYear == emInfo.CreatedYear && em.CreatedMonth == emInfo.CreatedMonth))
                        {
                            var emData = EmotionInfos.Where(em => em.Type.Equals(emInfo.Type) && em.CreatedYear == emInfo.CreatedYear && em.CreatedMonth == emInfo.CreatedMonth).First();
                            emData.Duration += emInfo.Duration;
                        }
                        else
                        {
                            EmotionInfos.Add(emInfo);
                        }
                    }
                }
            }
            else if (e.PropertyName.Equals(nameof(VideoDataViewModel.IsReset)))
            {
                VideoModels.Clear();
                EmotionInfos.Clear();
            }
        }

        private List<EmotionInfo> GetEmotionInfos(VideoModelInfo videoModelInfo)
        {
            var emotionInfos = new List<EmotionInfo>();
            foreach (var emotion in videoModelInfo.FirstVideoInsights.emotions) 
            {
                emotionInfos.Add(new EmotionInfo(emotion, videoModelInfo.Created));
            }

            return emotionInfos;
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
}

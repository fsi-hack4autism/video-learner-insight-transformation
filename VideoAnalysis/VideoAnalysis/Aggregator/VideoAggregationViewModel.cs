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

            }
            else if (e.PropertyName.Equals(nameof(VideoDataViewModel.IsReset)))
            {
                VideoModels.Clear();
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
}

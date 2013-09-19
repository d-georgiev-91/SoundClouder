namespace SoundClouder.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Controls;
    using System.Windows.Input;
    using SoundClouder.Behavior;
    using SoundClouder.Data;

    public class SearchViewModels : ViewModelBase
    {
        private ICommand searchTrackCommand;
        private ICommand playCommand;
        private ICommand stopCommand;
        private ICommand nextCommand;
        private ICommand prevCommand;

        private ObservableCollection<TrackViewModel> foundTracks;

        private TrackViewModel selectedTrack;

        public IEnumerable<TrackViewModel> FoundTracks
        {
            get
            {
                this.SelectedTrack = null;
                return this.foundTracks;
            }

            set
            {
                if (this.foundTracks == null)
                {
                    this.foundTracks = new ObservableCollection<TrackViewModel>();
                }

                this.foundTracks.Clear();

                if (value != null)
                {
                    foreach (TrackViewModel trackViewModel in value)
                    {
                        this.foundTracks.Add(trackViewModel);
                    }
                }

                this.OnPropertyChanged("FoundTracks");
            }
        }

        public ICommand NextCommand
        {
            get
            {
                if (this.nextCommand == null)
                {
                    this.nextCommand = new RelayCommand(new ExecuteDelegate(this.HandleNextCommad));
                }
                return this.nextCommand;
            }
        }

        public ICommand PlayCommand
        {
            get
            {
                if (this.playCommand == null)
                {
                    this.playCommand = new RelayCommand(new ExecuteDelegate(this.HandlePlayCommand));
                }
                return this.playCommand;
            }
        }

        public ICommand PrevCommand
        {
            get
            {
                if (this.prevCommand == null)
                {
                    this.prevCommand = new RelayCommand(new ExecuteDelegate(this.HandlePrevCommad));
                }
                return this.prevCommand;
            }
        }

        public string QueryString { get; set; }

        public ICommand SearchTrackCommand
        {
            get
            {
                if (this.searchTrackCommand == null)
                {
                    this.searchTrackCommand = new RelayCommand(new ExecuteDelegate(this.HandleSearchCommandAsync));
                }
                return this.searchTrackCommand;
            }
        }

        public TrackViewModel SelectedTrack
        {
            get
            {
                return this.selectedTrack;
            }
            set
            {
                this.selectedTrack = value;
                base.OnPropertyChanged("SelectedTrack");
            }
        }

        public ICommand StopCommand
        {
            get
            {
                if (this.stopCommand == null)
                {
                    this.stopCommand = new RelayCommand(new ExecuteDelegate(this.HandleStopCommand));
                }
                return this.stopCommand;
            }
        }

        private void HandleNextCommad(object parameter)
        {
            if (this.foundTracks == null || this.foundTracks.Count < 1)
            {
                return;
            }

            int currentTrackIndex = this.foundTracks.IndexOf(this.SelectedTrack);

            currentTrackIndex++;

            if (currentTrackIndex >= this.foundTracks.Count)
            {
                currentTrackIndex = 0;
            }

            this.SelectedTrack = this.foundTracks[currentTrackIndex];
        }

        private void HandlePlayCommand(object parameter)
        {
            if (this.SelectedTrack == null)
            {
                return;
            }

            var player = parameter as MediaElement;
            player.Play();
        }
        
        private void HandlePrevCommad(object parameter)
        {
            if (this.foundTracks == null || this.foundTracks.Count < 1)
            {
                return;
            }

            int curentTrackIndex = this.foundTracks.IndexOf(this.SelectedTrack);
            curentTrackIndex--;

            if (curentTrackIndex < 0)
            {
                curentTrackIndex = this.foundTracks.Count - 1;
            }

            this.SelectedTrack = this.foundTracks[curentTrackIndex];
        }

        private async void HandleSearchCommandAsync(object parameter)
        {
            this.FoundTracks = await DataPersister.GetTrack(this.QueryString);
        }

        private void HandleStopCommand(object parameter)
        {
            var player = parameter as MediaElement;
            player.Stop();
        }
    }
}
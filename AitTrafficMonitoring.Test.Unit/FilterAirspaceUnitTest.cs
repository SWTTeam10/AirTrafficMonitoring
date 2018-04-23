﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using AirTrafficMonitoring;


namespace AitTrafficMonitoring.Test.Unit
{
    [TestFixture]
    public class FilterAirspaceUnitTest
    {
        private IFilterAirspace _uut;
        private Track _track;
        private Track _track2;
        private List<Track> _tracklist;
        private ISortingTracks _sortingTracks;

        [SetUp]
        public void Setup()
        {
            _sortingTracks = Substitute.For<ISortingTracks>();
            _uut = new FilterAirspace(_sortingTracks);
            _tracklist = new List<Track>();
            _track2 = new Track { XCoordinate = 10000, YCoordinate = 10000, Altitude = 500 };

        }
        [Test]
        public void FilterTrack_TrackIsInAirspace_TrackIsOk()
        {
            _track = new Track { XCoordinate = 89999, YCoordinate = 89999, Altitude = 19999 };

            _tracklist.Add(_track);

            _uut.FilterTrack(_tracklist);

            _tracklist.Count.Equals(1);

        }

        [Test]
        public void FilterTrack_TrackIsInAirspace_TrackIsOk2()
        {
            _track = new Track { XCoordinate = 10001, YCoordinate = 10001, Altitude = 501 };

            _tracklist.Add(_track);

            _uut.FilterTrack(_tracklist);

            _tracklist.Count.Equals(1);
        }

        [Test]
        public void FilterTrack_XIsToLow_TrackDeleted()
        {
            _track = new Track { XCoordinate = 10000, YCoordinate = 10001, Altitude = 501 };

            _tracklist.Add(_track);

            _uut.FilterTrack(_tracklist);

            _tracklist.Count.Equals(0);
        }

        [Test]
        public void FilterTrack_YIsToLow_TrackDeleted()
        {
            _track = new Track { XCoordinate = 10001, YCoordinate = 10000, Altitude = 501 };

            _tracklist.Add(_track);

            _uut.FilterTrack(_tracklist);

            _tracklist.Count.Equals(0);
        }
        [Test]
        public void FilterTrack_AltIsToLow_TrackDeleted()
        {
            _track = new Track { XCoordinate = 10001, YCoordinate = 10001, Altitude = 500 };

            _tracklist.Add(_track);

            _uut.FilterTrack(_tracklist);

            _tracklist.Count.Equals(0);
        }

        [Test]
        public void FilterTrack_XIsToHigh_TrackDeleted()
        {
            _track = new Track { XCoordinate = 90000, YCoordinate = 89999, Altitude = 19999 };

            _tracklist.Add(_track);

            _uut.FilterTrack(_tracklist);

            _tracklist.Count.Equals(0);

        }


        [Test]
        public void FilterTrack_YIsToHigh_TrackDeleted()
        {
            _track = new Track { XCoordinate = 89999, YCoordinate = 90000, Altitude = 19999 };

            _tracklist.Add(_track);

            _uut.FilterTrack(_tracklist);

            _tracklist.Count.Equals(0);

        }

        [Test]
        public void FilterTrack_AltIsToHigh_TrackDeleted()
        {
            _track = new Track { XCoordinate = 89999, YCoordinate = 89999, Altitude = 20000 };

            _tracklist.Add(_track);

            _uut.FilterTrack(_tracklist);

            _tracklist.Count.Equals(0);

        }

        [Test]
        public void FilterTrack_TrackIsInAirspace_TrackIsOkm()
        {
            _track = new Track { XCoordinate = 89999, YCoordinate = 89999, Altitude = 19999 };

            _tracklist.Add(_track);

            _uut.FilterTrack(_tracklist);

            _sortingTracks.Received().SortTracksInAirspace(_tracklist);
        }
    }
}
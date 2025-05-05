using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ReactiveLikeApiDemo.Models;

namespace ReactiveLikeApiDemo.Services
{
    public static class BroadcastService
    {
        // Defines a 2-second delay to limit how often updates are pushed for each post.
        private static readonly TimeSpan BroadcastInterval = TimeSpan.FromSeconds(2);

        // A stream that groups posts by ID and rate-limits updates using Reactive Extensions (Rx).
        private static readonly IConnectableObservable<Post> _rateLimitedStream;

        static BroadcastService()
        {
            _rateLimitedStream = DataStore.PostSubject
                .GroupBy(post => post.Id) // Group updates by post ID
                .SelectMany(group => group.Throttle(BroadcastInterval)) // Emit only the latest after silence
                .Publish(); // Make the stream hot (shared)

            _rateLimitedStream.Connect(); // Start emitting values
        }

        public static IDisposable Subscribe(Action<Post> onNext)
        {
            // Allows consumers to receive rate-limited post updates.
            return _rateLimitedStream.Subscribe(onNext);
        }
    }
}

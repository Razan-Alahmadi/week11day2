
# Reactive Like Demo

## Overview
The **Reactive Like Demo** is a real-time like tracking system built using **ASP.NET Core** and **Reactive Extensions** (Rx.NET). It allows users to like posts, and the like count is updated in real-time using **Server-Sent Events (SSE)**. The system uses an in-memory data store to store posts and their like counts, and it broadcasts updates to subscribers with throttling using **Rx.NET**.

## Features
- **Like Post**: Users can like posts, and the like count is incremented.
- **Real-Time Updates**: Post updates (like count) are broadcasted to all connected clients using **SSE**.
- **Throttling**: Updates are throttled to send a maximum of one update per post every 2 seconds.

## Project Structure

1. **Models/Post.cs**: Defines the Post model with `Id`, `Content`, and `Likes`.
2. **Controllers/PostsController.cs**: Handles requests for liking posts and retrieving all posts.
3. **Controllers/SubscribeController.cs**: Handles subscriptions for real-time updates using SSE.
4. **Services/BroadcastService.cs**: Manages reactive stream for post updates and throttling.
5. **Frontend/index.html**: Simple HTML frontend that listens to real-time updates using SSE.



### Testing the API

- **Like a Post**: Send a `POST` request to `/api/posts/{id}/like` to like a specific post by its ID.
- **Subscribe for Real-Time Updates**: Open the `/api/subscribe` endpoint in your browser or Postman to start receiving real-time updates.

### Frontend
1. Open the `PostPage.html` file in your browser.
2. The frontend will display a list of posts with like buttons.
3. As you send likes to the backend, the frontend will update the like count in real-time using SSE.



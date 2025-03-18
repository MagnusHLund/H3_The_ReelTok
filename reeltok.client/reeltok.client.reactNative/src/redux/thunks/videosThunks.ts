import { createAsyncThunk } from '@reduxjs/toolkit'
import {
  addVideoToFeed,
  hasLikedVideo,
  Video,
  VideoRecommendationCategories,
} from '../slices/videosSlice'
import { GetVideosForFeedRequestDto } from '../../DTOs/videos/GetVideosForFeed/GetVideosForFeedRequestDto'
import httpService, { PayloadType } from '../../services/httpService'
import { HttpMethod } from '../../services/httpService'
import { addUserFromVideoThunk } from './usersThunks'

export const addVideoToFeedThunk = createAsyncThunk(
  'videos/addVideoToFeed',
  async (videosInFeed: Video[], { dispatch }) => {
    const httpMethod: HttpMethod = 'GET'
    const url: string = 'videos/feed'
    const body: GetVideosForFeedRequestDto = {
      amount: 2,
    }
    const payloadType: PayloadType = 'queryParameters'

    const videoFeed = await httpService<GetVideosForFeedRequestDto>({
      httpMethod,
      url,
      body,
      payloadType,
    })

    dispatch(addUserFromVideoThunk(videoFeed?.data.Videos))

    console.log(videoFeed?.data.Videos)

    const mappedVideos: Video[] = videoFeed?.data.Videos.map((v: any) => ({
      creatorUserId: v.VideoCreator.UserId,
      title: v.VideoDetails.Title,
      description: v.VideoDetails.Description,
      category: v.VideoDetails.Category,
      likes: v.VideoLikes.TotalLikes,
      hasLiked: v.VideoLikes.UserHasLikedVideo,
      videoId: v.VideoId,
      streamUrl: `https://cdn.reeltok.site/videos/${v.StreamPath}`,
      uploadedAt: v.UploadedAt,
    }))

    let newVideoFeed = [...videosInFeed, ...mappedVideos]

    newVideoFeed = [...new Map(newVideoFeed.map((video) => [video.videoId, video])).values()]

    if (newVideoFeed.length > 50) {
      newVideoFeed = newVideoFeed.slice(newVideoFeed.length - 50)
    }

    dispatch(addVideoToFeed(newVideoFeed))
  }
)

// TODO: Call api to add/remove the like
export const hasLikedVideoThunk = createAsyncThunk(
  'videos/hasLikedVideo',
  async (video: Video, { dispatch }) => {
    const updatedVideo = {
      ...video,
      hasLiked: !video.hasLiked,
      likes: video.hasLiked ? video.likes - 1 : video.likes + 1,
    }

    dispatch(hasLikedVideo(updatedVideo))
  }
)

import { addVideoToFeedThunk } from '../../redux/thunks/videosThunks'
import { addUserFromVideoThunk } from '../../redux/thunks/usersThunks'
import React, { useState, useEffect, useRef, useCallback, RefObject } from 'react'
import useAppDimensions from '../../hooks/useAppDimensions'
import { Platform, Animated, StyleSheet, View, FlatList } from 'react-native'
import { FlashList } from '@shopify/flash-list'
import { useRoute } from '@react-navigation/native'
import useAppSelector from '../../hooks/useAppSelector'
import useAppDispatch from '../../hooks/useAppDispatch'
import { Video } from '../../redux/slices/videosSlice'
import VideoPlayer from '../layout/video/VideoPlayer'
import UseOrientation from '../../hooks/useOrientation'
import VideoListApp from '../layout/video/VideoListApp'
import VideoListWeb from '../layout/video/VideoListWeb'

interface RenderItemProps {
  item: Video
  index: number
}

const VideoFeedScreen: React.FC = () => {
  const videos = useAppSelector((state) => state.videos.videos)
  const users = useAppSelector((state) => state.users.users)
  const dispatch = useAppDispatch()
  const { contentHeight } = useAppDimensions()
  const [currentlyDisplayedVideoIndex, setCurrentlyDisplayedVideoIndex] = useState(0)
  const [highestDisplayedVideoIndex, setHighestDisplayedVideoIndex] = useState(0)
  const videoFeedRef = React.useRef<FlashList<Video> | FlatList>(null)
  const route = useRoute()
  const orientation = UseOrientation(route.name, '')
  const videoRotation = useRef(new Animated.Value(0)).current

  useEffect(() => {
    if (videos.length === 0) {
      console.log('Fetching videos:', videos)
      dispatch(addVideoToFeedThunk(videos))
      dispatch(addUserFromVideoThunk(videos))
    }
  }, [videos, dispatch])

  useEffect(() => {
    if (currentlyDisplayedVideoIndex > highestDisplayedVideoIndex) {
      setHighestDisplayedVideoIndex(currentlyDisplayedVideoIndex)
      dispatch(addVideoToFeedThunk(videos))
      dispatch(addUserFromVideoThunk(videos))
      console.log(highestDisplayedVideoIndex)
    }
  }, [currentlyDisplayedVideoIndex])

  useEffect(() => {
    let rotationValue: number
    switch (orientation) {
      case 'left':
        rotationValue = 1 // 90deg
        break
      case 'right':
        rotationValue = -1 // -90deg
        break
      default:
        rotationValue = 0 // 0deg
    }

    Animated.timing(videoRotation, {
      toValue: rotationValue,
      duration: 300,
      useNativeDriver: true,
    }).start()
  }, [orientation])

  const rotationInterpolation = videoRotation.interpolate({
    inputRange: [-1, 0, 1],
    outputRange: ['-90deg', '0deg', '90deg'],
  })

  const handleViewableItemsChanged = useCallback(({ viewableItems }: any) => {
    if (viewableItems.length > 0) {
      setCurrentlyDisplayedVideoIndex(viewableItems[0].index)
    }
  }, [])

  const viewabilityConfig = {
    itemVisiblePercentThreshold: 10,
  }

  const handleAutoScroll = useCallback(() => {
    if (videoFeedRef.current) {
      const nextIndex = currentlyDisplayedVideoIndex + 1

      if (videos && nextIndex < videos.length) {
        setCurrentlyDisplayedVideoIndex(nextIndex)
        videoFeedRef.current.scrollToIndex({ index: nextIndex, animated: true })
      }

      dispatch(addVideoToFeedThunk(videos))
      dispatch(addUserFromVideoThunk(videos))
    }
  }, [currentlyDisplayedVideoIndex, videos, dispatch])

  const renderItem = useCallback(
    ({ item, index }: RenderItemProps) => {
      if (!item.creatorUserId || !item.videoId) {
        console.warn(
          `VideoCreator or UserId or VideoId not found for video with ID ${item.videoId}`
        )
        return null
      }

      // const user = users.find((u) => u.userId === item.creatorUserId)
      // if (!user) {
      //   console.warn(`User with ID ${item.creatorUserId} not found`)
      //   return null
      // }

      console.log('Rendering video with user: ' + item.creatorUserId)

      return (
        <Animated.View
          style={[styles.videoContainer, { transform: [{ rotate: rotationInterpolation }] }]}
        >
          <VideoPlayer
            videoDetails={item}
            onAutoScroll={handleAutoScroll}
            isDisplayed={currentlyDisplayedVideoIndex === index}
            userId={item.creatorUserId}
          />
        </Animated.View>
      )
    },
    [currentlyDisplayedVideoIndex, handleAutoScroll, users, rotationInterpolation]
  )

  const styles = StyleSheet.create({
    container: {
      flex: 1,
      backgroundColor: 'black',
    },
    videoContainer: {
      justifyContent: 'center',
    },
    videoFeed: {
      width: '100%',
      height: '100%',
      backgroundColor: 'black',
    },
  })

  return (
    <View style={styles.container}>
      {Platform.OS === 'web' ? (
        <VideoListWeb
          videoFeedRef={videoFeedRef as RefObject<FlatList>}
          videos={videos}
          renderItem={renderItem}
          contentHeight={contentHeight}
          style={styles.videoFeed}
          viewabilityConfig={viewabilityConfig}
          handleViewableItemsChanged={handleViewableItemsChanged}
        />
      ) : (
        <VideoListApp
          videoFeedRef={videoFeedRef as RefObject<FlashList<Video>>}
          videos={videos}
          renderItem={renderItem}
          contentHeight={contentHeight}
          style={styles.videoFeed}
          viewabilityConfig={viewabilityConfig}
          handleViewableItemsChanged={handleViewableItemsChanged}
        />
      )}
    </View>
  )
}

export default VideoFeedScreen

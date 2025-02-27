import { addVideoToFeedThunk } from '../../redux/thunks/videosThunks'
import React, { useState, useEffect, useCallback } from 'react'
import useAppDimensions from '../../hooks/useAppDimensions'
import { StyleSheet, View, FlatList } from 'react-native'
import useAppSelector from '../../hooks/useAppSelector'
import useAppDispatch from '../../hooks/useAppDispatch'
import { Video } from '../../redux/slices/videosSlice'
import VideoPlayer from '../Layout/video/VideoPlayer'
import Navbar from '../Layout/common/Navbar'

interface RenderItemProps {
  item: Video
  index: number
}

// TODO: Ensure that there are always 3 videos loaded, ahead of the users current position.
// TODO: Cleanup rendered VideoPlayers, once their data is no longer saved in redux
// TODO: Ensure that addVideosToFeed is dispatched when manual scrolling
// TODO: Fix bug with it not being able to scroll more than 48 times.
// TODO: Limit scroll speed. It can scroll SUPER fast on web.
const VideoFeedScreen: React.FC = () => {
  const videos = useAppSelector((state) => state.videos.videos)
  const dispatch = useAppDispatch()
  const { contentHeight } = useAppDimensions()
  const [currentlyDisplayedVideoIndex, setCurrentlyDisplayedVideoIndex] = useState(0)
  const [highestDisplayedVideoIndex, setHighestDisplayedVideoIndex] = useState(0)
  const videoFeedRef = React.useRef<FlatList>(null)

  useEffect(() => {
    if (videos.length === 0) {
      dispatch(addVideoToFeedThunk(videos))
    }
  }, [videos, dispatch])

  useEffect(() => {
    if (currentlyDisplayedVideoIndex > highestDisplayedVideoIndex) {
      setHighestDisplayedVideoIndex(currentlyDisplayedVideoIndex)
      dispatch(addVideoToFeedThunk(videos))
      console.log(highestDisplayedVideoIndex)
    }
  }, [currentlyDisplayedVideoIndex])

  const handleViewableItemsChanged = useCallback(({ viewableItems }: any) => {
    if (viewableItems.length > 0) {
      setCurrentlyDisplayedVideoIndex(viewableItems[0].index)
    }
  }, [])

  const viewabilityConfig = {
    itemVisiblePercentThreshold: 50,
  }

  const handleAutoScroll = useCallback(() => {
    if (videoFeedRef.current) {
      const nextIndex = currentlyDisplayedVideoIndex + 1

      if (videos && nextIndex < videos.length) {
        setCurrentlyDisplayedVideoIndex(nextIndex)
        videoFeedRef.current.scrollToIndex({ index: nextIndex, animated: true })
      }

      dispatch(addVideoToFeedThunk(videos))
    }
  }, [currentlyDisplayedVideoIndex, videos, dispatch])

  const renderItem = useCallback(
    ({ item, index }: RenderItemProps) => (
      <View style={styles.videoContainer}>
        <VideoPlayer
          videoDetails={item}
          onAutoScroll={handleAutoScroll}
          isDisplayed={currentlyDisplayedVideoIndex === index}
        />
      </View>
    ),
    [currentlyDisplayedVideoIndex, handleAutoScroll]
  )

  return (
    <View style={styles.container}>
      <FlatList
        ref={videoFeedRef}
        data={videos}
        renderItem={renderItem}
        keyExtractor={(item) => item.videoId}
        pagingEnabled
        showsVerticalScrollIndicator={false}
        style={[styles.videoFeed, { height: contentHeight }]}
        decelerationRate="fast"
        disableIntervalMomentum
        snapToInterval={contentHeight}
        snapToAlignment="center"
        onViewableItemsChanged={handleViewableItemsChanged}
        viewabilityConfig={viewabilityConfig}
        getItemLayout={(_, index) => ({
          length: contentHeight,
          offset: contentHeight * index,
          index,
        })}
      />
      <Navbar />
    </View>
  )
}

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

export default VideoFeedScreen

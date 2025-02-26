import { addVideoToFeedThunk } from '../../redux/thunks/videosThunks'
import { useAppSelector } from '../../hooks/useAppSelector'
import { useAppDispatch } from '../../hooks/useAppDispatch'
import useAppDimensions from '../../hooks/useAppDimensions'
import React, { useState, useEffect, useRef } from 'react'
import { StyleSheet, View, FlatList } from 'react-native'
import VideoPlayer from '../Layout/video/VideoPlayer'
import Navbar from '../Layout/common/Navbar'

// TODO: handle automatically playing video and pausing video on web, when scrolling, when it scrolls.
// ^^^^  Currently only works on the app.
const VideoFeedScreen: React.FC = () => {
  const videos = useAppSelector((state) => state.videos.videos)
  const dispatch = useAppDispatch()
  const { contentHeight } = useAppDimensions()
  const [currentIndex, setCurrentIndex] = useState(0)
  const videoFeedRef = useRef<FlatList>(null)

  useEffect(() => {
    if (!videos || videos.length === 0) {
      dispatch(addVideoToFeedThunk())
    }
  }, [videos, dispatch])

  const handleViewableItemsChanged = useRef(({ viewableItems }: any) => {
    if (viewableItems.length > 0) {
      setCurrentIndex(viewableItems[0].index)
    }
  }).current

  const handleNextVideo = () => {
    dispatch(addVideoToFeedThunk())
  }

  const handleAutoScroll = () => {
    if (videoFeedRef.current) {
      const nextIndex = currentIndex + 1

      if (videos && nextIndex < videos.length) {
        setCurrentIndex(nextIndex)
        videoFeedRef.current.scrollToIndex({ index: nextIndex, animated: true })
      }

      handleNextVideo()
    }
  }

  const renderItem = ({ item, index }: any) => (
    <View style={styles.videoContainer}>
      <VideoPlayer
        videoDetails={videos?.[index]}
        onNextVideo={handleAutoScroll}
        isDisplayed={currentIndex === index}
      />
    </View>
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
        onScroll={handleNextVideo}
        style={[styles.videoFeed, { height: contentHeight }]}
        decelerationRate={'fast'}
        disableIntervalMomentum
        snapToInterval={contentHeight}
        snapToAlignment={'center'}
        onViewableItemsChanged={handleViewableItemsChanged}
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

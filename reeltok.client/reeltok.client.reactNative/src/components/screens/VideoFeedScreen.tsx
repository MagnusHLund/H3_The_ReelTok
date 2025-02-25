import { addVideoToFeedThunk } from '../../redux/thunks/videosThunks'
import { useAppSelector } from '../../hooks/useAppSelector'
import { useAppDispatch } from '../../hooks/useAppDispatch'
import useAppDimensions from '../../hooks/useAppDimensions'
import React, { useState, useEffect, useRef } from 'react'
import { StyleSheet, View, FlatList } from 'react-native'
import VideoPlayer from '../Layout/video/VideoPlayer'
import Navbar from '../Layout/common/Navbar'

const VideoFeedScreen = () => {
  const videos = useAppSelector((state) => state.videos.videos)
  const dispatch = useAppDispatch()
  const { contentHeight } = useAppDimensions()

  const [currentIndex, setCurrentIndex] = useState(0)
  const viewabilityConfig = useRef({ viewAreaCoveragePercentThreshold: 50 }).current

  useEffect(() => {
    if (videos === undefined || videos.length === 0) {
      dispatch(addVideoToFeedThunk(videos))
    }
  }, [videos, dispatch])

  const handleViewableItemsChanged = useRef(({ viewableItems }: any) => {
    if (viewableItems.length > 0) {
      setCurrentIndex(viewableItems[0].index)
    }
  }).current

  const handleNextVideo = () => {
    dispatch(addVideoToFeedThunk(videos))
  }

  const renderedVideo = ({ item, index }: any) => (
    <View style={styles.videoContainer}>
      <VideoPlayer
        videoDetails={videos?.[currentIndex] ?? undefined}
        onNextVideo={handleNextVideo}
      />
    </View>
  )
  return (
    <View style={styles.container}>
      <FlatList
        data={videos}
        renderItem={renderedVideo}
        keyExtractor={(item) => item.videoId}
        pagingEnabled
        showsVerticalScrollIndicator={false}
        onScroll={() => dispatch(addVideoToFeedThunk(videos))}
        style={[styles.videoFeed, { height: contentHeight }]}
        decelerationRate={'fast'}
        disableIntervalMomentum={true}
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

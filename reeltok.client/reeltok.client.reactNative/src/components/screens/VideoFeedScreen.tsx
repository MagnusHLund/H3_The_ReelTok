import { StyleSheet, View, FlatList, Dimensions } from 'react-native'
import { addVideoToFeedThunk } from '../../redux/thunks/videosThunks'
import { useAppSelector } from '../../hooks/useAppSelector'
import { useAppDispatch } from '../../hooks/useAppDispatch'
import React, { useState, useRef, useEffect } from 'react'
import VideoPlayer from '../Layout/video/VideoPlayer'
import Navbar from '../Layout/common/Navbar'

const VideoFeedScreen = () => {
  const videos = useAppSelector((state) => state.videos.videos)
  const dispatch = useAppDispatch()

  const [currentIndex, setCurrentIndex] = useState(0)
  const viewabilityConfig = { viewAreaCoveragePercentThreshold: 50 }

  useEffect(() => {
    // Effect to run when videos state updates
    if (videos === undefined || videos.length === 0) {
      dispatch(addVideoToFeedThunk(videos))
    }
    console.log('Videos state has updated:', videos)
  }, [videos, dispatch])

  const onViewableItemsChanged = ({ viewableItems }: any) => {
    if (viewableItems.length > 0) {
      setCurrentIndex(viewableItems[0].index)
    }
  }

  const handleNextVideo = () => {}

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
        style={styles.video}
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
    height: Dimensions.get('window').height - 60, // Adjust height to leave space for navbar (assuming navbar is 60px)
    justifyContent: 'center',
  },
  video: {
    width: '100%',
    height: '100%',
  },
})

export default VideoFeedScreen

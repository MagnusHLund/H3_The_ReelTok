import { View, ScrollView, StyleSheet, Text } from 'react-native'
import CustomButton from '../../input/CustomButton'
import CustomImage from '../common/CustomImage'
import React, { useEffect } from 'react'
import useAppSelector from '../../../hooks/useAppSelector'

type ProfileVideo = {
  source: any
  uploadedAt: string
}

const VideoGallery: React.FC = () => {
  const amountOfVideos = 10 // Define the amount of videos
  const user = useAppSelector((state) => state.users.myUser)
  const videos = useAppSelector((state) => state.videos.videos)

  const userVideos = videos.filter((video) => video.creatorUserId === user.userId)

  const videoThumbnails = userVideos.map((video) => ({
    ...video,
    thumbnailUrl: video.streamUrl.replace('.MP4', '.jpg'),
  }))

  useEffect(() => {
    console.log()
  }, [])

  return (
    <ScrollView contentContainerStyle={styles.VideoContainer}>
      {videoThumbnails.map((video) => (
        <View key={video.videoId} style={styles.videoItem}>
          <CustomButton onPress={() => {}} transparent>
            <CustomImage
              height={80}
              width={80}
              borderRadius={15}
              source={{ uri: video.thumbnailUrl }}
              resizeMode="cover"
            />
          </CustomButton>
        </View>
      ))}
    </ScrollView>
  )
}

const styles = StyleSheet.create({
  VideoContainer: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'space-between',
    backgroundColor: 'lightgrey',
    height: '100%',
  },
  videoItem: {
    width: '33%',
    marginBottom: 10,
  },
  videoImage: {
    width: 80,
    height: 100,
  },
})

export default VideoGallery

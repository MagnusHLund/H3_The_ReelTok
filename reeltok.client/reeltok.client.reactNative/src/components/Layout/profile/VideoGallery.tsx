import { View, ScrollView, StyleSheet } from 'react-native'
import CustomButton from '../../input/CustomButton'
import CustomImage from '../common/CustomImage'
import React from 'react'

type ProfileVideo = {
  source: any
  uploadedAt: string
}

const VideoGallery: React.FC = () => {
  const amountOfVideos = 10 // Define the amount of videos
  const videoArray = fetchVideosFromApi() // Assume this function fetches the video array from the API

  function fetchVideosFromApi(): ProfileVideo[] {
    // Mock API call
    return Array.from({ length: amountOfVideos }, (_, index) => ({
      id: index + 1,
      source: { uri: `https://example.com/video${index + 1}.mp4` },
      uploadedAt: new Date(Date.now() - index * 1000 * 60 * 60 * 24).toISOString(), // Mock uploadedAt date
    }))
  }

  // Sort videos by uploadedAt property
  const sortedVideos = [...videoArray].sort(
    (a, b) => new Date(b.uploadedAt).getTime() - new Date(a.uploadedAt).getTime()
  )

  return (
    <ScrollView contentContainerStyle={styles.VideoContainer}>
      {sortedVideos.map((video, index) => (
        <View key={index} style={styles.videoItem}>
          <CustomButton onPress={() => {}} transparent>
            <CustomImage
              height={80}
              width={80}
              borderRadius={15}
              source={video.source}
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
    padding: 10,
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

import { PersistGate } from 'redux-persist/integration/react'
import { persistor, store } from './redux/store'
import { Provider } from 'react-redux'
import React, { useCallback } from 'react'
import { useVideoPlayer, VideoView } from 'expo-video'
import { FlatList, View, StyleSheet } from 'react-native'

const sampleData = [
  {
    id: '1',
    url: 'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4',
  },
  {
    id: '2',
    url: 'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4',
  },
  {
    id: '3',
    url: 'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4',
  },
]

export default function App() {
  const players = sampleData.map((item) =>
    useVideoPlayer(item.url, (player) => {
      player.loop = true
      player.play()
    })
  )

  const renderItem = useCallback(
    ({ item, index }) => {
      const player = players[index]

      return (
        <View style={styles.videoContainer}>
          <VideoView
            player={player}
            nativeControls={true}
            contentFit="contain"
            style={styles.video}
          />
        </View>
      )
    },
    [players]
  )

  return (
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        <FlatList
          data={sampleData}
          renderItem={renderItem}
          keyExtractor={(item) => item.id}
          style={styles.list}
        />
      </PersistGate>
    </Provider>
  )
}

const styles = StyleSheet.create({
  list: {
    flex: 1,
    backgroundColor: 'black',
  },
  videoContainer: {
    width: '100%',
    height: 300, // Adjust the height as needed
    marginBottom: 20,
  },
  video: {
    width: '100%',
    height: '100%',
  },
})

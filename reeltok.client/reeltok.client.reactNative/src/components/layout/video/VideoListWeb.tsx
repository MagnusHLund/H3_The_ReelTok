import React from 'react'
import type { ListRenderItem, ViewToken } from 'react-native'
import { Video } from '../../../redux/slices/videosSlice'
import { FlatList } from 'react-native'

interface VideoListWebProps {
  videoFeedRef: React.RefObject<FlatList>
  videos: Video[]
  renderItem: ListRenderItem<Video>
  contentHeight: number
  viewabilityConfig: object
  handleViewableItemsChanged: (info: {
    viewableItems: ViewToken<Video>[]
    changed: ViewToken<Video>[]
  }) => void
  style?: object
}

const VideoListWeb: React.FC<VideoListWebProps> = ({
  videoFeedRef,
  videos,
  renderItem,
  contentHeight,
  viewabilityConfig,
  handleViewableItemsChanged,
  style,
}) => {
  return (
    <FlatList
      ref={videoFeedRef}
      data={videos}
      renderItem={renderItem}
      removeClippedSubviews={true}
      keyExtractor={(item) => item.videoId}
      pagingEnabled
      showsVerticalScrollIndicator={false}
      style={[style, { height: contentHeight }]}
      decelerationRate="fast"
      disableIntervalMomentum
      snapToInterval={contentHeight}
      snapToAlignment="center"
      onViewableItemsChanged={handleViewableItemsChanged}
      viewabilityConfig={viewabilityConfig}
      getItemlayout={(_, index) => ({
        length: contentHeight,
        offset: contentHeight * index,
        index,
      })}
    />
  )
}

export default VideoListWeb

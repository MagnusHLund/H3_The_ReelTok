import React from 'react'
import { FlashList } from '@shopify/flash-list';
import type { ListRenderItem, ViewToken } from '@shopify/flash-list'
import { Video } from '../../../redux/slices/videosSlice'

interface VideoListAppProps {
  videoFeedRef: React.RefObject<FlashList<Video>>
  videos: Video[]
  renderItem: ListRenderItem<Video>
  contentHeight: number
  viewabilityConfig: object
  handleViewableItemsChanged: (info: {
    viewableItems: ViewToken[]
    changed: ViewToken[]
  }) => void 
  style?: object
}

const VideoListApp: React.FC<VideoListAppProps> = ({
  videoFeedRef,
  videos,
  renderItem,
  contentHeight,
  viewabilityConfig,
  handleViewableItemsChanged,
  style,
}) => {
  return (
    <FlashList<Video>
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
      estimatedItemSize={contentHeight}
    />
  )
}

export default VideoListApp

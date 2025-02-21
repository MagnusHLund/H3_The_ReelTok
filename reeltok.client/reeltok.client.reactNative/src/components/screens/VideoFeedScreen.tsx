
import React, { useState, useRef } from 'react';
import { StyleSheet, View, FlatList, Dimensions } from 'react-native';
import Video from '../Layout/video/Video';
import Navbar from '../Layout/common/Navbar';
import CommentSection from '../Layout/video/CommentsSection';
const { height } = Dimensions.get('window');

const VideoFeedScreen = () => {
  const [currentIndex, setCurrentIndex] = useState(0);
  const [showComments, setShowComments] = useState(false);
  const viewabilityConfig = useRef({ viewAreaCoveragePercentThreshold: 50 }).current;

  const videoSources = [
    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4",
    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/SubaruOutbackOnStreetAndDirt.mp4",
    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4",
  ];

  const videoComments = {
    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4": [
      { text: "Amazing view!", profilePictureUrl: "https://i.pinimg.com/236x/2d/c7/b9/2dc7b9725100be062bc9b9e8276d84bd.jpg", username: "NatureLover" },
      { text: "Epic escape!", profilePictureUrl: "https://i.pinimg.com/236x/2d/c7/b9/2dc7b9725100be062bc9b9e8276d84bd.jpg", username: "Explorer99" }
    ],
    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/SubaruOutbackOnStreetAndDirt.mp4": [
      { text: "Love this car!", profilePictureUrl: "https://i.pinimg.com/236x/2d/c7/b9/2dc7b9725100be062bc9b9e8276d84bd.jpg", username: "CarFanatic" },
      { text: "Great commercial!", profilePictureUrl: "https://i.pinimg.com/236x/2d/c7/b9/2dc7b9725100be062bc9b9e8276d84bd.jpg", username: "AutoGeek" }, 
    ],
    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4": [
      { text: "So artistic!", profilePictureUrl: "https://i.pinimg.com/236x/2d/c7/b9/2dc7b9725100be062bc9b9e8276d84bd.jpg", username: "FilmBuff" },
      { text: "Beautiful animation!", profilePictureUrl: "https://i.pinimg.com/236x/2d/c7/b9/2dc7b9725100be062bc9b9e8276d84bd.jpg", username: "Cinephile" }
    ]
  };

  const handleViewableItemsChanged = useRef(({ viewableItems }) => {
    if (viewableItems.length > 0) {
      setCurrentIndex(viewableItems[0].index);
    }
  }).current;

  const commentsAmount = 10;

  return (
    <>
      <FlatList
        data={videoSources}
        renderItem={({ item, index }) =>
          <Video source={item} isFocused={index === currentIndex} onShowComments={() => setShowComments(true)} />
        }
        keyExtractor={(_, index) => index.toString()}
        decelerationRate='fast'
        disableIntervalMomentum={true}
        style={{ backgroundColor: 'black' }}
        showsVerticalScrollIndicator={false}
        onViewableItemsChanged={handleViewableItemsChanged}
        viewabilityConfig={viewabilityConfig}
        snapToInterval={height}
        snapToAlignment="center"
        getItemLayout={(_, index) => ({
          length: height,
          offset: height * index,
          index,
        })}
      />
      <CommentSection comments={videoComments[videoSources[currentIndex]] || []} commentsAmount={commentsAmount} showComments={showComments} onClose={() => setShowComments(false)}/>
      <Navbar/>
    </>
  );
};

export default VideoFeedScreen;


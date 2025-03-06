import { View, Image, StyleSheet } from 'react-native';
import UploadedVideo from './../upload/UploadedVideo';
import React from 'react';

interface CapturedContentProps {
  uri: string;
  cameraMode: 'picture' | 'video';
  contentHeight: number;
  fullWidth: number;
}

const CapturedContent: React.FC<CapturedContentProps> = ({ uri, cameraMode, contentHeight, fullWidth }) => {
  return (
    <View style={[styles.contentContainer, { height: contentHeight }]}>
      {cameraMode === 'picture' && uri && (
        <View style={[styles.pictureContainer, { height: contentHeight, width: fullWidth }]}>
          <Image
            source={{ uri }}
            resizeMode="contain"
            style={[styles.picture, { height: contentHeight, width: fullWidth }]}
          />
        </View>
      )}
      {cameraMode === 'video' && uri !== '' && <UploadedVideo uri={uri} />}
    </View>
  );
};

const styles = StyleSheet.create({
  contentContainer: {
    height: '100%',
    width: '100%',
    position: 'absolute',
    top: 0,
    bottom: 0,
    left: 0,
    right: 0,
    zIndex: 1,
  },
  pictureContainer: {
    width: '100%',
    height: '100%',
    position: 'absolute',
    backgroundColor: 'black',
  },
  picture: {
    borderRadius: '50%',
  },
});

export default CapturedContent;
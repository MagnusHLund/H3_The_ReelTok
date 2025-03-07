import { View, StyleSheet } from 'react-native';
import CustomButton from '../../input/CustomButton';
import MaterialCommunityIcons from '@expo/vector-icons/MaterialCommunityIcons';
import AntDesign from '@expo/vector-icons/AntDesign';
import React from 'react';

interface CameraControlsProps {
  uri: string;
  uploadVideo: (uri: string | null) => void;
  resetUri: () => void;
}

const CameraControls: React.FC<CameraControlsProps> = ({ uri, uploadVideo, resetUri }) => {
  return (
    <View style={styles.contentButtons}>
      <CustomButton onPress={resetUri} transparent>
        <MaterialCommunityIcons name="restore" size={50} color={'white'} />
      </CustomButton>
      <CustomButton onPress={() => uploadVideo(uri)} transparent>
        <AntDesign name="arrowright" size={50} color="white" />
      </CustomButton>
    </View>
  );
};

const styles = StyleSheet.create({
  contentButtons: {
    position: 'absolute',
    bottom: 44,
    width: '100%',
    alignItems: 'center',
    flexDirection: 'row',
    justifyContent: 'space-between',
    zIndex: 1,  
  },
});

export default CameraControls;
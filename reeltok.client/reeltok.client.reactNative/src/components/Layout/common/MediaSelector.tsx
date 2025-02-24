import { StyleSheet, View } from 'react-native'
import React from 'react'
import CustomButton from '../../input/CustomButton'
import Entypo from '@expo/vector-icons/Entypo'
import MaterialIcons from '@expo/vector-icons/MaterialIcons'

import { NativeStackNavigationProp } from '@react-navigation/native-stack'
import { useNavigation } from '@react-navigation/native'
import { useCameraPermissions } from 'expo-camera';

export default function MediaSelector() {
  const navigation = useNavigation<NativeStackNavigationProp<any>>()
  const [, requestPermission] = useCameraPermissions();
  return (
    <View style={styles.container}>
      <CustomButton widthPercentage={0.45} onPress={async () => { await requestPermission(); navigation.replace('Camera'); }}>
        <Entypo name="camera" size={24} color="white" />
      </CustomButton>
      <CustomButton widthPercentage={0.45} onPress={console.log}>
        <MaterialIcons name="photo-library" size={24} color="white" />
      </CustomButton>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    top: -40,
    backgroundColor: '#262626',
    width: '100%',
    paddingTop: 10,

    borderRadius: 5,
    marginBottom: 5,
    paddingLeft: 5,
    paddingRight: 5,
    paddingBottom: 15,
  },
})

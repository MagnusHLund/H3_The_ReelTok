// The navbar needs to be able to be at the bottom of the screen, in vertical mode, but on the right side of the screen while in horizontal mode.
// The icons inside the navbar should turn to the correct axis, for the given screen orientation.

import React from 'react';
import { View, StyleSheet } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import Ionicons from '@expo/vector-icons/Ionicons';

import CustomButton from '../../input/CustomButton';

const Navbar: React.FC = () => {
  const navigation = useNavigation<NativeStackNavigationProp>();

  const styles = StyleSheet.create({
      container: {
        display: 'flex',
        position: 'absolute',
        bottom: 0,
        justifyContent: 'space-evenly',
        alignItems: 'center',
        flexDirection: 'row',
        width: '100%',
        height: '6%',
        backgroundColor: 'black',
        zIndex: 110,
      },
  });
  
  return (
    <>
      <View style={styles.container}>
        <CustomButton transparent={true} onPress={() => navigation.navigate('VideoFeed')} > <Ionicons name="play" size={32} color="white" /> </CustomButton>
        <CustomButton transparent={true} onPress={() => console.log("hello")}> <Ionicons name="add" size={32} color="white" /> </CustomButton>
        <CustomButton transparent={true} onPress={() => navigation.navigate('Profile')} > <Ionicons name="person-circle-sharp" size={32} color="white" /></CustomButton>
      </View>
    </>
  );
};

export default Navbar;


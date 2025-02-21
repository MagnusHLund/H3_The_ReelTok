import React from 'react'
import { View, Image, Text, StyleSheet, ScrollView } from 'react-native'
import CustomButton from '../input/CustomButton'
import Navbar from '../Layout/common/Navbar'
import { useNavigation } from '@react-navigation/native'
import { NativeStackNavigationProp } from '@react-navigation/native-stack'
import { Ionicons } from '@expo/vector-icons'

interface UserProfileScreenProps {
  Username: string
}

const UserProfileScreen: React.FC<UserProfileScreenProps> = ({ Username }) => {
  const navigation = useNavigation<NativeStackNavigationProp<any>>()

  const videos = [
    {
      id: 1,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 2,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 3,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 4,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 5,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 6,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 7,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 8,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 9,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 10,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 11,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 12,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 13,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 14,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 5,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 6,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 7,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 8,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 9,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 10,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 11,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 12,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 13,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 14,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 1,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 2,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 3,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 4,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 5,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 6,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 7,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 8,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 9,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 10,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 11,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 12,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 13,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 14,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 5,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 6,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 7,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 8,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 9,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 10,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 11,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 12,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 13,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
    {
      id: 14,
      source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
    },
  ]

  return (
    <>
      <View style={styles.ProfileContainer}>
        <View>
          <CustomButton onPress={() => {}} transparent>
            <Image
              style={[styles.ProfilePicture, { resizeMode: 'cover' }]}
              source={require('./../../../assets/images/placeholders/profile-default-img.png')}
            />
          </CustomButton>
        </View>
      </View>
      <View style={styles.mainContainer}>
        <CustomButton widthPercentage={0.22} onPress={() => {}} transparent>
          <Text>posts</Text>
        </CustomButton>
        <CustomButton widthPercentage={0.22} onPress={() => {}} transparent>
          <Text>Followers</Text>
        </CustomButton>
        <CustomButton widthPercentage={0.22} onPress={() => {}} transparent>
          <Text>Following</Text>
        </CustomButton>
        <CustomButton
          widthPercentage={0.07}
          onPress={() => navigation.replace('Settings')}
          transparent
        >
          <Ionicons name="cog" size={24} color="black" />
        </CustomButton>
      </View>
      <ScrollView contentContainerStyle={styles.VideoContainer}>
        {videos.map((video, index) => (
          <View key={index} style={styles.videoItem}>
            <CustomButton onPress={() => {}} transparent>
              <Image style={styles.videoImage} source={video.source} />
            </CustomButton>
          </View>
        ))}
      </ScrollView>
      <Navbar />
    </>
  )
}

const styles = StyleSheet.create({
  mainContainer: {
    flexDirection: 'row',
    width: '100%',
    justifyContent: 'center',
    top: -70,
    left: 40,
  },
  ProfilePicture: {
    width: 80,
    height: 80,
    borderRadius: 50,
  },
  ProfileName: {
    fontSize: 20,
    fontWeight: 'bold',
    top: -30,
  },
  ProfileContainer: {
    flexDirection: 'row',
    justifyContent: 'flex-start',
    alignItems: 'center',
    paddingTop: 60,
  },
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

export default UserProfileScreen

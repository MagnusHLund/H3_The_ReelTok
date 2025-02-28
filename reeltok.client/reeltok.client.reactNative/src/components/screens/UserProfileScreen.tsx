import { View, Image, Text, StyleSheet, ScrollView } from 'react-native'
import useAppNavigation from '../../hooks/useAppNavigation'
import useTranslation from '../../hooks/useTranslations'
import CustomButton from '../input/CustomButton'
import { Ionicons } from '@expo/vector-icons'
import React from 'react'

const UserProfileScreen: React.FC = () => {
  const navigateToScreen = useAppNavigation()
  const t = useTranslation()

  const videos = Array.from({ length: 30 }, (_, index) => ({
    id: index + 1,
    source: require('./../../../assets/images/placeholders/Oskarotto3.jpg'),
  }))

  // TODO: @MarcusL00 refactor this please. The component has to be broken down into smaller components.

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
        {/* TODO: Remove button for posts and fix styling */}
        <CustomButton widthPercentage={0.22} onPress={() => {}} transparent>
          <Text>{t('profile.posts')}</Text>
        </CustomButton>
        <CustomButton
          widthPercentage={0.22}
          onPress={() => {
            navigateToScreen('Subscribers')
          }}
          transparent
        >
          <Text>{t('common.subscribers')}</Text>
        </CustomButton>
        <CustomButton
          widthPercentage={0.22}
          onPress={() => navigateToScreen('Subscriptions')}
          transparent
        >
          <Text>{t('common.subscriptions')}</Text>
        </CustomButton>
        <CustomButton
          widthPercentage={0.07}
          onPress={() => navigateToScreen('Settings')}
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

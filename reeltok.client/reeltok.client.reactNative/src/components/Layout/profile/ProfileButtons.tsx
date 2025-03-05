import useAppNavigation from '../../../hooks/useAppNavigation'
import useTranslation from '../../../hooks/useTranslations'
import FontAwesome5 from '@expo/vector-icons/FontAwesome5'
import { View, Text, StyleSheet } from 'react-native'
import CustomButton from '../../input/CustomButton'
import React from 'react'

const ProfileButtons = () => {
  const navigateToScreen = useAppNavigation()
  const t = useTranslation()

  return (
    <View style={styles.mainContainer}>
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
        widthPercentage={0.3}
        onPress={() => navigateToScreen('Subscriptions')}
        transparent
      >
        <Text>{t('common.subscriptions')}</Text>
      </CustomButton>
    </View>
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
})

export default ProfileButtons

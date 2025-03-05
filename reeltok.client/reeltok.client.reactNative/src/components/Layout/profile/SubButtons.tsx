import useAppNavigation from '../../../hooks/useAppNavigation'
import useTranslation from '../../../hooks/useTranslations'
import { View, Text, StyleSheet } from 'react-native'
import CustomButton from '../../input/CustomButton'
import React from 'react'

interface SubButtonsProps {
  subscriberCount: number
  subscriptionCount: number
}

const SubButtons: React.FC<SubButtonsProps> = ({ subscriberCount, subscriptionCount }) => {
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
        <Text style={styles.subcount}>{subscriberCount}</Text>
        <Text style={styles.subtext}>{t('common.subscribers')}</Text>
      </CustomButton>
      <CustomButton
        widthPercentage={0.3}
        onPress={() => navigateToScreen('Subscriptions')}
        transparent
      >
        <Text style={styles.subcount}>{subscriptionCount}</Text>
        <Text style={styles.subtext}>{t('common.subscriptions')}</Text>
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
  subtext: {
    fontWeight: '400',
  },
  subcount: {
    fontSize: 16,
    fontWeight: 'bold',
  },
  ProfileContainer: {
    flexDirection: 'row',
    justifyContent: 'flex-start',
    alignItems: 'center',
    paddingTop: 60,
  },
})

export default SubButtons

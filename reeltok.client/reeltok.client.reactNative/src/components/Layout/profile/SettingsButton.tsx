import useAppNavigation from '../../../hooks/useAppNavigation'
import CustomButton from '../../input/CustomButton'
import { FontAwesome5 } from '@expo/vector-icons'
import { View } from 'react-native'
import React from 'react'

const SettingsButton: React.FC = () => {
  const navigateToScreen = useAppNavigation()

  return (
    <View>
      <CustomButton widthPercentage={0.07} onPress={() => navigateToScreen('Settings')} transparent>
        <FontAwesome5 name="cog" size={21} color="black" />
      </CustomButton>
    </View>
  )
}

export default SettingsButton

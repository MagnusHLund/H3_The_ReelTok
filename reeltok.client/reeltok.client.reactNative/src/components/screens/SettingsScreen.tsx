import RecommendationsSettingsSection from '../Layout/settings/sections/RecommendationsSettingsSection'
import UserDetailsSettingsSection from '../Layout/settings/sections/UserDetailsSettingsSection'
import LanguageSettingsSection from '../Layout/settings/sections/LanguageSettingsSection'
import LogOutSettingsSection from '../Layout/settings/sections/LogOutSettingsSection'
import { ScrollView, View } from 'react-native'
import Header from '../Layout/common/Header'
import React from 'react'

const SettingsScreen: React.FC = () => {
  // TODO: Fix issue with VirtualizedLists not being allowed inside of ScrollView (open dropdown to trigger error)
  return (
    <View>
      <Header title="Settings" />
      <ScrollView>
        <UserDetailsSettingsSection />
        <LanguageSettingsSection />
        <RecommendationsSettingsSection />
        <LogOutSettingsSection />
      </ScrollView>
    </View>
  )
}

export default SettingsScreen

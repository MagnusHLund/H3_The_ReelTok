import RecommendationsSettingsSection from '../Layout/settings/sections/RecommendationsSettingsSection'
import UserDetailsSettingsSection from '../Layout/settings/sections/UserDetailsSettingsSection'
import LanguageSettingsSection from '../Layout/settings/sections/LanguageSettingsSection'
import LogOutSettingsSection from '../Layout/settings/sections/LogOutSettingsSection'
import { FlatList, View } from 'react-native'
import Header from '../Layout/common/Header'
import React from 'react'

const SettingsScreen: React.FC = () => {
  const sections = [
    { key: 'UserDetails', component: <UserDetailsSettingsSection /> },
    { key: 'Language', component: <LanguageSettingsSection /> },
    { key: 'Recommendations', component: <RecommendationsSettingsSection /> },
    { key: 'LogOut', component: <LogOutSettingsSection /> },
  ]

  // We are forced to use the FlatList here, because the dropdown we use, uses scroll view.
  // You should not use 2 scroll views inside each other.
  return (
    <View style={{ flex: 1 }}>
      <Header title="Settings" />
      <FlatList
        data={sections}
        renderItem={({ item }) => item.component}
        keyExtractor={(item) => item.key}
      />
    </View>
  )
}

export default SettingsScreen

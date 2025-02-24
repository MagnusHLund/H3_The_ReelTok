import RecommendationsSettingsSection from '../Layout/settings/sections/RecommendationsSettingsSection'
import UserDetailsSettingsSection from '../Layout/settings/sections/UserDetailsSettingsSection'
import BackButtonSettingsSection from '../Layout/settings/sections/BackButtonSettingsSection'
import LanguageSettingsSection from '../Layout/settings/sections/LanguageSettingsSection'
import LogOutSettingsSection from '../Layout/settings/sections/LogOutSettingsSection'
import useTranslation from '../../hooks/useTranslations'
import { FlatList, View } from 'react-native'
import Header from '../Layout/common/Header'
import React from 'react'

const SettingsScreen: React.FC = () => {
  const t = useTranslation()

  const sections = [
    // { key: 'BackButton', component: <BackButtonSettingsSection /> },
    { key: 'Recommendations', component: <RecommendationsSettingsSection /> },
    { key: 'UserDetails', component: <UserDetailsSettingsSection /> },
    { key: 'Language', component: <LanguageSettingsSection /> },
    { key: 'LogOut', component: <LogOutSettingsSection /> },
  ]

  // TODO: The message below is not true anymore, due to creating our own dropdown. I think its most fitting to revert back to ScrollView
  // We are forced to use the FlatList here, because the dropdown we use, uses scroll view.
  // You should not use 2 scroll views inside each other.
  return (
    <View style={{ flex: 1 }}>
      <BackButtonSettingsSection />
      <Header title={t('settings.settings')} />
      <FlatList
        data={sections}
        renderItem={({ item }) => item.component}
        keyExtractor={(item) => item.key}
      />
    </View>
  )
}

export default SettingsScreen

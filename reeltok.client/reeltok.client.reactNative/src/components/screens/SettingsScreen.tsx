import RecommendationsSettingsSection from '../Layout/settings/sections/RecommendationsSettingsSection'
import UserDetailsSettingsSection from '../Layout/settings/sections/UserDetailsSettingsSection'
import LanguageSettingsSection from '../Layout/settings/sections/LanguageSettingsSection'
import LogOutSettingsSection from '../Layout/settings/sections/LogoutSettingsSection'
import useTranslation from '../../hooks/useTranslations'
import { ScrollView, View } from 'react-native'
import Header from '../Layout/common/Header'
import React from 'react'

// TODO: Revert the ScrollView back into a FlatList. You can go back in the file version and see how it used to be.
// ^^^^  I thought it wouldn't be an issue to use ScrollView, but it is. Open a settings dropdown and see.
const SettingsScreen: React.FC = () => {
  const t = useTranslation()

  return (
    <View style={{ flex: 1 }}>
      <Header showBackButton title={t('settings.settings')} />
      <ScrollView>
        <UserDetailsSettingsSection />
        <RecommendationsSettingsSection />
        <LanguageSettingsSection />
        <LogOutSettingsSection />
      </ScrollView>
    </View>
  )
}

export default SettingsScreen

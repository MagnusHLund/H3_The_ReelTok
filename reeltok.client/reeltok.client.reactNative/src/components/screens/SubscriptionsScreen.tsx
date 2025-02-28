import useTranslation from '../../hooks/useTranslations'
import { ScrollView, StyleSheet } from 'react-native'
import UserList from '../Layout/common/UserList'
import Header from '../Layout/common/Header'
import React from 'react'

const SubscriptionsScreen: React.FC = () => {
  const t = useTranslation()

  const Subscription: {
    GuidId: string
    PictureUrl: string
    ProfileUrl: string
    Username: string
  }[] = [
    {
      GuidId: '550e8400-e29b-41d4-a716-446655440000',
      PictureUrl:
        'https://gratisography.com/wp-content/uploads/2024/11/gratisography-augmented-reality-1170x780.jpg',
      ProfileUrl: 'https://example.com/user1',
      Username: 'UserOne',
    },
    {
      GuidId: 'c56a4180-65aa-42ec-a945-5fd21dec0538',
      PictureUrl:
        'https://gratisography.com/wp-content/uploads/2024/11/gratisography-augmented-reality-1170x780.jpg',
      ProfileUrl: 'https://example.com/user2',
      Username: 'UserTwo',
    },
    {
      GuidId: '550e8400-e29b-41d4-a716-446655440000',
      PictureUrl:
        'https://gratisography.com/wp-content/uploads/2024/11/gratisography-augmented-reality-1170x780.jpg',
      ProfileUrl: 'https://example.com/user1',
      Username: 'UserThree',
    },
    {
      GuidId: 'c56a4180-65aa-42ec-a945-5fd21dec0538',
      PictureUrl:
        'https://gratisography.com/wp-content/uploads/2024/11/gratisography-augmented-reality-1170x780.jpg',
      ProfileUrl: 'https://example.com/user2',
      Username: 'UserFourabcdedsaadgasdferbasdfargavdasdfsasdf',
    },
    {
      GuidId: 'c56a4180-65aa-42ec-a945-5fd21dec0538',
      PictureUrl:
        'https://gratisography.com/wp-content/uploads/2024/11/gratisography-augmented-reality-1170x780.jpg',
      ProfileUrl: 'https://example.com/user2',
      Username: 'UserTwo',
    },
    {
      GuidId: '550e8400-e29b-41d4-a716-446655440000',
      PictureUrl:
        'https://gratisography.com/wp-content/uploads/2024/11/gratisography-augmented-reality-1170x780.jpg',
      ProfileUrl: 'https://example.com/user1',
      Username: 'UserThree',
    },
    {
      GuidId: 'c56a4180-65aa-42ec-a945-5fd21dec0538',
      PictureUrl:
        'https://gratisography.com/wp-content/uploads/2024/11/gratisography-augmented-reality-1170x780.jpg',
      ProfileUrl: 'https://example.com/user2',
      Username: 'UserFourabcdedsaadgasdferbasdfargavdasdfsasdf',
    },
    {
      GuidId: 'c56a4180-65aa-42ec-a945-5fd21dec0538',
      PictureUrl:
        'https://gratisography.com/wp-content/uploads/2024/11/gratisography-augmented-reality-1170x780.jpg',
      ProfileUrl: 'https://example.com/user2',
      Username: 'UserTwo',
    },
    {
      GuidId: '550e8400-e29b-41d4-a716-446655440000',
      PictureUrl:
        'https://gratisography.com/wp-content/uploads/2024/11/gratisography-augmented-reality-1170x780.jpg',
      ProfileUrl: 'https://example.com/user1',
      Username: 'UserThree',
    },
    {
      GuidId: 'c56a4180-65aa-42ec-a945-5fd21dec0538',
      PictureUrl:
        'https://gratisography.com/wp-content/uploads/2024/11/gratisography-augmented-reality-1170x780.jpg',
      ProfileUrl: 'https://example.com/user2',
      Username: 'UserFourabcdedsaadgasdferbasdfargavdasdfsasdf',
    },
  ]

  return (
    <ScrollView>
      <Header showBackButton title={t('common.subscriptions')} />
      <UserList users={Subscription} />
    </ScrollView>
  )
}

export default SubscriptionsScreen

const styles = StyleSheet.create({})

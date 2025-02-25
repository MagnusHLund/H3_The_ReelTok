import { ScrollView, StyleSheet, Text, View } from 'react-native'
import React from 'react'
import Header from '../Layout/common/Header'
import useTranslation from '../../hooks/useTranslations'
import ProfilePicture from '../Layout/profile/ProfilePicture'
import UserList from '../Layout/common/UserList'

const SubscribersScreen = () => {
  const t = useTranslation()

  const Subscribers: {
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
      <Header showBackButton title={t('subscribers.subscribers')} />
      <UserList users={Subscribers} />
    </ScrollView>
  )
}

export default SubscribersScreen

const styles = StyleSheet.create({})

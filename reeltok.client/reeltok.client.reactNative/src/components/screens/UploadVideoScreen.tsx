import React from 'react'
import UploadVideo from '../Layout/common/UploadVideo'
import Header from '../Layout/common/Header'

const UploadVideoScreen = () => {
  return (
    <>
      <Header showBackButton title={'Upload video'} />
      <UploadVideo />
    </>
  )
}

export default UploadVideoScreen

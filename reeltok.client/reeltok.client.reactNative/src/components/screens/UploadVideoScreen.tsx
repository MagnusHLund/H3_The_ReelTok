import React from 'react'
import UploadVideo from '../layout/common/UploadVideo'
import Header from '../layout/common/Header'

const UploadVideoScreen = () => {
  return (
    <>
      <Header showBackButton title={'Upload video'} />
      <UploadVideo />
    </>
  )
}

export default UploadVideoScreen

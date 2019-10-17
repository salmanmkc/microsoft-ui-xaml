﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#pragma once

#include "ScrollingPresenter.h"
#include "ScrollCompletedEventArgs.g.h"

class ScrollCompletedEventArgs :
    public winrt::implementation::ScrollCompletedEventArgsT<ScrollCompletedEventArgs>
{
public:
    ScrollCompletedEventArgs()
    {
        SCROLLER_TRACE_VERBOSE(nullptr, TRACE_MSG_METH, METH_NAME, this);
    }

    ~ScrollCompletedEventArgs()
    {
        SCROLLER_TRACE_VERBOSE(nullptr, TRACE_MSG_METH, METH_NAME, this);
    }

    // IScrollCompletedEventArgs overrides
    winrt::ScrollInfo ScrollInfo();
    ScrollingPresenterViewChangeResult Result();

    void OffsetsChangeId(int32_t offsetsChangeId);
    void Result(ScrollingPresenterViewChangeResult result);

private:
    int32_t m_offsetsChangeId{ -1 };
    ScrollingPresenterViewChangeResult m_result{ ScrollingPresenterViewChangeResult::Completed };
};